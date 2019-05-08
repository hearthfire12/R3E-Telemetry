namespace Infrastructure.Analyzer
{
    using Models;

    public interface IAnalyzer
    {
        void Analyze(RequiredModel model);
    }
}