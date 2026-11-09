namespace RaceDay_API.Services
{
    public interface IResultsServices
    {
        string enter_results(string finish_time, string race_Positions,
            string Avg_pace, string role);

        string retrieve(int id);

        string update_result(int id, string finish_time,
            string race_Positions, string Avg_pace, string role);

        string get_all_results();
    }
}