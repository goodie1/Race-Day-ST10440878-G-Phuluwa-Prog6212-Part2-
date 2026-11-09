namespace RaceDay_API.Services
{
    public interface ICategoriesServices
    {
        string category(string category_type, string distance, int age, string role);

        string return_info(int id);

        string get_all_categories();
    }
}