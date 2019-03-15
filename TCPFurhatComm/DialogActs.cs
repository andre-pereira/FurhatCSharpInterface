using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class DialogActs {

    public string behaviorFileName;
    Random randomGenerator;
    Dictionary<string, List<Behavior>> dialogActs;

    public DialogActs(string fileLocation, bool ignoreFirstLine = true)
    {
        randomGenerator = new Random();
        dialogActs = new Dictionary<string, List<Behavior>>();
        LoadBehaviors(fileLocation, ignoreFirstLine);
    }

    private int random(int minValue, int maxValue)
    {
        return randomGenerator.Next(minValue, maxValue);
    }

    private bool random(double probability)
    {
        var generated = randomGenerator.NextDouble();
        if (generated < probability)
            return true;
        else return false;
    }

    public Behavior getRandomBehavior(string dialogAct)
    {
        if (dialogActs.ContainsKey(dialogAct))
            return getRandomBehavior(dialogActs[dialogAct]);
        else return null;
    }

    internal Behavior getRandomBehavior(List<Behavior> utteranceListAll)
    {
        double randomNumber = randomGenerator.NextDouble();

        int minNumTimesSaid = utteranceListAll.Min(x => x.numTimesSaid);
        List<Behavior> lessUsedUtteranceList = utteranceListAll.Where(x => x.numTimesSaid == minNumTimesSaid).ToList();

        randomNumber = randomNumber * lessUsedUtteranceList.Count;
        double valueSoFar = 0;
        foreach (var item in lessUsedUtteranceList)
        {
            valueSoFar += 1;
            if (randomNumber <= valueSoFar)
            {
                item.numTimesSaid++;
                return item;
            }
        }

        return null;
    }

    internal void LoadBehaviors(string filepath, bool ignoreFirstLine = true)
    {
        var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using (StreamReader sr = new StreamReader(fs))
        {
            string line;
            if (ignoreFirstLine)
                line = sr.ReadLine();

            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split('\t');
                Behavior behavior = new Behavior(data[1].Replace("\"\"", "\""), data[2], data[3], 0);
                if (dialogActs.ContainsKey(data[0]))
                    dialogActs[data[0]].Add(behavior);
                else
                {
                    dialogActs[data[0]] = new List<Behavior>();
                    dialogActs[data[0]].Add(behavior);
                }
            }
        }
    }

    internal List<string> GetBlockOfStrings(string category)
    {
        List<string> toReturn = new List<string>();
        foreach (var item in dialogActs[category])
        {
            toReturn.Add(item.text);
        }
        return toReturn;
    }

}

public class Behavior
{
    public string text;
    public string emotion;
    public string customEvent;
    public int numTimesSaid;

    public Behavior(string text, string emotion, string customEvent, int numTimesSaid)
    {
        this.text = text;
        this.emotion = emotion;
        this.customEvent = customEvent;
        this.numTimesSaid = numTimesSaid;
    }
}
