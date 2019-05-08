namespace TelemetryTests.SupportedGames
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using NUnit.Framework;

    // https://stackoverflow.com/questions/2560258/how-to-pass-an-event-to-a-method
    // https://stackoverflow.com/questions/2567047/unit-testing-that-an-event-is-raised-in-c-using-reflection/2697721#2697721
    // https://web.archive.org/web/20160322150001/http://gojisoft.com/blog/2010/04/22/event-sequence-unit-testing-part-1/

    public class EventsTester
    {
        public EventsTester(Object eventPublisher)
        {
            _eventPublisher = eventPublisher;
            _eventNameToCountMap = new Dictionary<string, int>();
        }

        private readonly Object _eventPublisher;
        private readonly Dictionary<string, int> _eventNameToCountMap;

        //delegate void OnIncrement();
        //private readonly OnIncrement _increment;

        private void HandleEvent()
        {
            //var events = _eventPublisher.GetType().GetEvents();

            //StackTrace stackTrace = new StackTrace();
            //var previousFrame = stackTrace.GetFrame(1);
            //var parentMethod = previousFrame.GetMethod();




            StackTrace stackTrace = new StackTrace();
            var previousFrame = stackTrace.GetFrame(1);

            var t = previousFrame.GetType();
            var parentMethod = previousFrame.GetMethod();
            var events = parentMethod.ReflectedType.GetEvents();

            var obj = parentMethod.ReflectedType;

            for (int i = 0; i < events.Length; i++)
            {
                Delegate retDelegate = null;
                //FieldInfo fi = parentMethod.ReflectedType.GetField(events[i].Name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
                FieldInfo fi = obj.GetField(events[i].Name, 
                    BindingFlags.NonPublic | 
                    BindingFlags.Static |
                    BindingFlags.Instance | 
                    BindingFlags.FlattenHierarchy |
                    BindingFlags.IgnoreCase);

                object value = fi.GetValue(obj);
                if (value is Delegate)
                    retDelegate = (Delegate)value;
                else if (value != null) // value may be just object
                {
                    PropertyInfo pi = obj.GetType().GetProperty("Events",
                        BindingFlags.NonPublic |
                        BindingFlags.Instance);
                    if (pi != null)
                    {
                        EventHandlerList eventHandlers = pi.GetValue(obj) as EventHandlerList;
                        if (eventHandlers != null)
                        {
                            retDelegate = eventHandlers[value];
                        }
                    }
                }

                Delegate del = (Delegate)fi.GetValue(obj);
                var list = del.GetInvocationList();
                //if (list.Contains(parentMethod))
                //{
                //    _eventNameToCountMap[parentMethod.ReflectedType.Name]++;
                //}
            }
        }


        public void RegisterEvent(EventPublisher obj, string name)
        {
            EventInfo eventInfo = obj.GetType().GetEvent(name);
            _eventNameToCountMap.Add(name, 0);
            MethodInfo method = this.GetType().GetMethod("HandleEvent", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, null, method);

            // Raise the event
            eventInfo.AddEventHandler(obj, handler);
        }

        public void AssertEvent(string name, int raisedCount)
        {
            Assert.That(_eventNameToCountMap[name], Is.EqualTo(raisedCount));
        }
    }
}