namespace RaceDay_API.Services
{
    public interface IEventsServices
    {

        string create_event( string event_name,string date,string time, string event_Type, string Event_description,string location,string race_route,string weather,int organiser_id, string role);
        string event_details(int id);
        string event_update(int id, string event_name, string date, string time, string event_Type, string Event_description, string location, string race_route, string weather, string role);
        string events_deleted(int id, string role);
        string get_organiser_events(int organiser_id);
        string get_all_events();
    }
}
