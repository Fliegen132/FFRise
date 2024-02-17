using System.Collections.Generic;

public class ResourcesCounter : IService
{
    private Dictionary<string, int> resources;
    private Dictionary<string, int> addResources;
    private ViewResources viewResources;

    public ResourcesCounter()
    {
        resources = new Dictionary<string, int>
        {
            ["gold"] = 0,
            ["wood"] = 0,
            ["stone"] = 0,
            ["energy"] = 0,
        };

        addResources = new Dictionary<string, int>()
        {
            ["gold"] = 0,
            ["wood"] = 0,
            ["stone"] = 0,
            ["energy"] = 0,
        };
        viewResources = ServiceLocator.Current.Get<ViewResources>();
    }

    public bool BuyForResource(string resource, int price)
    {
        if (resources.ContainsKey(resource) && resources[resource] >= price)
        {
            resources[resource] -= price;
            Apply();
            return true;
        }
        return false;
    }

    public bool CheckResource(string resource, int price)
    {
        if (resources.ContainsKey(resource) && resources[resource] >= price)
        {
            return true;
        }

        return false;
    }

    public void AddResource(string resource, int value)
    {
        if (!addResources.ContainsKey(resource))
        {
            addResources[resource] = 0;
        }
        addResources[resource] += value;
    }

    public void Apply()
    {
        foreach (var kvp in addResources)
        {
            string resource = kvp.Key;
            int value = kvp.Value;
            if (resources.ContainsKey(resource) && value > 0)
            {
                resources[resource] += value;
            }
        }

        addResources.Clear();
        viewResources.UpdateText();
    }

    public Dictionary<string, int> GetAll()
    {
        return resources;
    }
}