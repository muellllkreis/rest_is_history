using System.Collections;
using System.Collections.Generic;
using System;

public class Environment
{
    private int water, common, mountain, desert, forest, snow, caves;
    private int temperature { get; set; }
    private bool seasons { get; set; }
    public List<Resource> resources { get; set; }

    public Environment() {
        resources = new List<Resource>();
        Random rand = new Random();
        int[] ratios = new int[7];
        int max = 100;
        int total = 0;
        for(int i = 0; i < ratios.Length; i++) {
            ratios[i] = rand.Next((max - total) / (ratios.Length - i));
        }
        this.water = ratios[6];
        this.common = ratios[5];
        this.mountain = ratios[4];
        this.desert = ratios[3];
        this.forest = ratios[2];
        this.snow = ratios[1];
        this.caves = ratios[0];

        resources.Add(new MaterialResource("STONE"));
        resources.Add(new MaterialResource("WOOD"));
    }

}
