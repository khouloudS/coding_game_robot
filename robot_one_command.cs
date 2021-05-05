using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Player
{


    public static string Solve(int clawPos, int[] boxes, bool boxInClaw)
    {
         // Write your code here
        // To debug: Console.Error.WriteLine("Debug messages...");
        int sum=0;
        for(int i=0; i<boxes.Length; i++){
            sum=sum+boxes[i];
        }
        if(boxInClaw){
            sum=sum+1;
        }
        int[] target = new int[boxes.Length];
        for(int i=0; i<target.Length; i++){
            target[i]=(int)sum/target.Length;
        }
        for(int i=0; i<sum%target.Length; i++){
            target[i]=target[i]+1;
        }
        int min_pos=boxes.Length;
        int next_pos=0;

        if(boxInClaw){
            
                for(int i=0; i<boxes.Length; i++){
                    if(boxes[i]<target[i] && Math.Abs(clawPos-i)<min_pos){
                        min_pos=Math.Abs(clawPos-i);
                        next_pos=i;
                    }
                }
                if(clawPos<next_pos){
                    return "RIGHT";
                }
                else if(clawPos>next_pos){
                    return "LEFT";
                }
                else{return "PLACE";}

        }
        else{
                    for(int i=0; i<boxes.Length; i++){
                    if(boxes[i]>target[i] && Math.Abs(clawPos-i)<min_pos){
                        min_pos=Math.Abs(clawPos-i);
                        next_pos=i;
                    }

                }
                if(clawPos<next_pos){
                    return "RIGHT";
                }
                else if(clawPos>next_pos){
                    return "LEFT";
                }
                else{return "PICK";}

        }
    }

    /* Ignore and do not change the code below */
    #region
    static void Main(string[] args)
    {
        string[] inputs;

        // game loop
        while (true)
        {
            int clawPos = int.Parse(Console.ReadLine());
            bool boxInClaw = Console.ReadLine() != "0";
            int stacks = int.Parse(Console.ReadLine());
            int[] boxes = new int[stacks];
            inputs = Console.ReadLine().Split(' ');
            for (int i = 0; i < stacks; i++)
            {
                boxes[i] = int.Parse(inputs[i]);
            }
            var stdtoutWriter = Console.Out;
            Console.SetOut(Console.Error);
            string action = Solve(clawPos, boxes, boxInClaw);
            Console.SetOut(stdtoutWriter);
            Console.WriteLine(action);
        }
    }
    #endregion
}
