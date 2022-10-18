using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Week5Proj;

public static class WeaponsHelper
{
    private const string Dnd5Eapi = "https://www.dnd5eapi.co/api/";
    private static readonly List<Weapon> WeaponsList;

    static WeaponsHelper()
    {
        WeaponsList = GetWeapons() as List<Weapon> ?? throw new InvalidOperationException();
    }

    public static Weapon GetWeapon()
    {
        var rand = new Random();
        return WeaponsList[rand.Next(WeaponsList.Count)];
    }

    private static IEnumerable<Weapon> GetWeapons()
    {
        return !File.Exists("weapons.csv") ? GetWeaponsFromApi() : GetWeaponsFromCsv();
    }

    private static List<Weapon> GetWeaponsFromCsv()
    {
        throw new NotImplementedException();
    }

    private static List<Weapon> GetWeaponsFromApi()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(Dnd5Eapi);

        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = client.GetAsync("equipment-categories/martial-weapons").Result;
        dynamic? data = null;
        if (response.IsSuccessStatusCode)
        {
            data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
        }

        if (data == null) throw new NullReferenceException("API Call returned null");
        var retList = new List<Weapon>();
        foreach (dynamic? item in data.equipment)
        {
            string name = item.name;
            retList.Add(new Weapon(name));
        }
        
        response = client.GetAsync("equipment-categories/simple-weapons").Result;
        data = null;
        if (response.IsSuccessStatusCode)
        {
            data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
        }

        if (data == null) throw new NullReferenceException("API Call returned null");
        foreach (dynamic? item in data.equipment)
        {
            string name = item.name;
            retList.Add(new Weapon(name));
        }

        var weaponsToRemove = new List<Weapon>();
        foreach (Weapon weapon in retList)
        {
            try
            {
                string index = weapon.Name.Replace(", ", "-").Replace(" ", "-").ToLower();
                response = client.GetAsync($"equipment/{index}").Result;
                data = null;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                }

                if (data == null) throw new NullReferenceException($"API Call returned null. Index: {index}");
                string dice = data.damage.damage_dice;
                weapon.MinDamage = Convert.ToInt32(new string(dice[0], 1));
                weapon.MaxDamage = Convert.ToInt32(new string(dice[^1], 1));

                var properties = new JArray(data.properties);
                //TODO: Extract two-handed property from array and set in object
            }
            catch
            {
                weaponsToRemove.Add(weapon);
            }
        }

        foreach (Weapon weapon in weaponsToRemove)
        {
            retList.Remove(weapon);
        }

        ExportCsv(retList, "weapons");

        return retList;
    }

    private static void ExportCsv<T>(List<T> genericList, string fileName)
    {
        var sb = new StringBuilder();
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string finalPath = Path.Combine(basePath, fileName+".csv");
        var header = "";
        PropertyInfo[] info = typeof(T).GetProperties();
        if (!File.Exists(finalPath))
        {
            FileStream file = File.Create(finalPath);
            file.Close();
            header = typeof(T).GetProperties().Aggregate(header, (current, prop) => current + prop.Name + "; ");
            header = header[..^2];
            sb.AppendLine(header);
            TextWriter sw = new StreamWriter(finalPath, true);
            sw.Write(sb.ToString());
            sw.Close();
        }

        foreach (T obj in genericList)
        {
            sb = new StringBuilder();
            string line = info.Aggregate("", (current, prop) => current + (prop.GetValue(obj, null) + "; "));
            line = line[..^2];
            sb.AppendLine(line);
            TextWriter sw = new StreamWriter(finalPath, true);
            sw.Write(sb.ToString());
            sw.Close(); 
        }
    }
}