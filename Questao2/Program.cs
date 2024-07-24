using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;
        int page = 1;
        bool hasMorePages = true;

        using (HttpClient client = new HttpClient())
        {
            while (hasMorePages)
            {
                string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}";
                HttpResponseMessage response = client.GetAsync(url).Result;
                string responseData = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(responseData);

                foreach (var match in json["data"])
                {
                    totalGoals += int.Parse(match["team1goals"].ToString());
                }

                int totalPages = int.Parse(json["total_pages"].ToString());
                if (page >= totalPages)
                {
                    hasMorePages = false;
                }
                page++;
            }

            page = 1;
            hasMorePages = true;

            while (hasMorePages)
            {
                string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={page}";
                HttpResponseMessage response = client.GetAsync(url).Result;
                string responseData = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(responseData);

                foreach (var match in json["data"])
                {
                    totalGoals += int.Parse(match["team2goals"].ToString());
                }

                int totalPages = int.Parse(json["total_pages"].ToString());
                if (page >= totalPages)
                {
                    hasMorePages = false;
                }
                page++;
            }
        }

        return totalGoals;
    }
}