namespace RaceDay_API.Services
{
    public interface IAuthenticationServices
    {
        string Sign_up(string name, string surname, string email, string Cell_Number, string role, string password);

        string Log_in(string name, string email,string password);

        string retrive_Organizer(int id);

        string update_Profile(int id, string name, string surname, string email, string Cell_Number, string password);

        string remove_Organizer(int id, string role);
    }
}
