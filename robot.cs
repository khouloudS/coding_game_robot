using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Player
{

    string cmd_ret="";
    public int get_lower(int[] boxes, int[] target, int clawPos){
        int min_pos=boxes.Length;
        for(int i=0; i<boxes.Length; i++){
            if(boxes[i]<target[i] && Math.abs(clawPos-i)<min_pos){
                min_pos=i;
            }
        }
        return min_pos;
    }

    public bool verification(int[] boxes, int[] target ){
        for(int i=0; i<boxes.Length; i++){
            if(boxes[i]!=target[i]){
                return true;
            }
        }
        return false;
    }

    public int get_higher(int[] boxes, int[] target,int clawPos)
    {
        int min_pos=boxes.Length;
        for(int i=0; i<boxes.Length; i++){
            if(boxes[i]>target[i] && Math.abs(clawPos-i)<min_pos){
                min_pos=i;
            }
        }
        return min_pos;
    }

    public int move(int start, int end, bool boxInClaw){
        string cmd;
        if(boxInClaw){
            cmd="PLACE|";
            boxInClaw=false;
        }
        else{
            cmd="PICK|";
            boxInClaw=true;
        }
        if(start < end){
            for(int i=0; i<(end-start); i++){
                cmd_ret=cmd_ret+"RIGHT|";
                cmd_ret=cmd_ret+cmd;
            }
        }
        else{
            for(int i=0; i<(start-end); i++){
                cmd_ret=cmd_ret+"LEFT|";
                cmd_ret=cmd_ret+cmd;
            }
        }



        return end;


    }

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

        Player player= new Player();
        int lower, higher;


        if(boxInClaw){
            lower=player.get_lower(boxes, target,clawPos);
            clawPos=player.move(clawPos, lower, boxInClaw);
            boxes[lower]=boxes[lower]+1;

        }
        while(player.verification(boxes,target)){
           
            higher=player.get_higher(boxes,target,clawPos);
            clawPos=player.move(clawPos, higher, boxInClaw);
            boxes[higher]=boxes[higher]-1;
            lower=player.get_lower(boxes,target,clawPos);
            clawPos=player.move(clawPos, lower, boxInClaw);
            boxes[lower]=boxes[lower]+1;

        }
        for(int i=0; i<boxes.Length; i++){
            Console.Error.WriteLine(target[i]);
            Console.Error.WriteLine(boxes[i]);
        }
        return player.cmd_ret;
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