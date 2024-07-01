using Newtonsoft.Json.Linq;

internal class Program
{
    static async Task Main(string[] args)
    {
        await TeamTotalGolsByYear("Paris Saint-Germain", 2013);
        await TeamTotalGolsByYear("Chelsea", 2014);
    }

    static async Task TeamTotalGolsByYear(string team, int year)
    {
        int totalGoals = await GetTotalGoalsByYear(team, year);
        Console.WriteLine($"Team {team} scored {totalGoals} goals in {year}");
    }

    static async Task<int> GetTotalGoalsByYear(string team, int year)
    {
        int totalGoals = 0;
        totalGoals += await GetGoalsResponse(team, year, "team1"); //Mandante
        totalGoals += await GetGoalsResponse(team, year, "team2"); //Visitante
        return totalGoals;
    }

    //Retorna o numero de gols do time na temporada como Mandante team1 ou Visitante team2
    static async Task<int> GetGoalsResponse(string team, int year, string teamPosition)
    {
        int totalGoals = 0;
        int currentPage = 1;
        bool hasPages = true;

        while (hasPages)
        {
            string apiUrl = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{teamPosition}={team}&page={currentPage}";
            string response = await GetApiResponse(apiUrl);
            JObject json = JObject.Parse(response);

            foreach (var match in json["data"])
            {
                totalGoals += int.Parse(match[$"{teamPosition}goals"].ToString());
            }

            int totalPages = int.Parse(json["total_pages"].ToString());
            hasPages = currentPage < totalPages;
            currentPage++;
        }

        return totalGoals;
    }

    private static async Task<string> GetApiResponse(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
