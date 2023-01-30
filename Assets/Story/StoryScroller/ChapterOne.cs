using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOne : MonoBehaviour
{
    StoryScroller scroll;

    void Start()
    {
        scroll = FindObjectOfType<StoryScroller>();
        AllActs();

    }


    void AllActs()
    {
        ActOne();
        Catch3FishIntermission();
        ActTwo();
        Catch3FishIntermission();
        ActThree();
        Catch3FishIntermission();
        ActFour();
        Catch3FishIntermission();
        ActFive();
        ActSix();
        Catch3FishIntermission();
        ActSeven();
        Catch3FishIntermission();
        ActEight();
    }

    //convenience methods
    void Write(string s, float delayAfter = 0, int extraNewLines = 0)
    {
        scroll.Write(s, delayAfter);

        if (extraNewLines > 0)
            scroll.NewLine(extraNewLines);
    }

    void NewLine(int howMany = 1)
    {
        scroll.NewLine(howMany);
    }

    void ClearScreen()
    {
        scroll.Write("µ");
    }

    //will write the content into the buffer for the StoryScroller
    // █ is ascii 219, and it is a 
    //                             h e s i t a t i o n
    void ActOne()
    {
        Write("Once...", 0.5f);
        Write("Upon...", 0.5f);
        Write("A Time...", 0.5f);

        Write("A nerd wanted to make █a game");
        Write("A great game█ that people would play⌐⌐⌐⌐⌐enjoy");
        Write("and somehow KNOW that it's okay to have fun");
        Write("it's okay to get really deep into something");
        Write("BUT!");
        Write("To do that the nerd needed something█ elusive");
        Write("Something very, very rare⌐⌐⌐⌐⌐RARE", 0.5f);
        Write("The nerd needed a story", 0.5f);
        Write("Because without a story");
        Write("the nerd had nothing", 0.5f);
        Write("no idea, meant no game");
        Write("and█ without a story");
        Write("what was the point███ :(", 1.3f);

        Write("what's the point in anything?", 1.3f);
        Write("then one day.....", 2f);

    }



    void Catch3FishIntermission()
    {
        ClearScreen();
        Write("Catch 3 FISH!");
        NewLine();
        Write("                         .");
        Write("                      /     .");
        Write("                  o /          .");
        Write("              ,   M              .  ");
        Write("~~~~~~~~~~~~~}{}\\--------------/~~=~~~~~~~~~~~~~~~");
        Write("                                  .");
        Write("                                  .");
        Write("                      ><<<))*>    j");
        Write("                          /");
        Write("                          ><<<))*>   ");
        Write("                             /");
        Write("                  ><<<))*>");
        Write("                     /");
        Write("                                      0");
        Write("                           ><<<))*>");
        Write("                              /");

    }



    void ActTwo()
    {
        ClearScreen();
        Write("Great Job");
        Write("See that's all it took to feel like you accomplished something");
        NewLine();
        NewLine();
        NewLine();
        NewLine();
        Write("██but that's not a story.");
        NewLine();
        NewLine();
        NewLine();

        Write("The story has to be deeper.");
        Write("You can't JUST have game mechanics! You NEED A STORY.");
        Write("AAahhh, but alas, the nerd still could not think of a story.", 0.5f);
        Write("You see, it wasn't that the nerd didn't have an interesting life");
        Write("Lots of things had happened to the nerd.");
        Write("Some of those things might be deemed interesting enough by");
        Write("some other nerd,");
        Write("or something.");
        Write("??????");
        Write("Like...", 0.5f);

        Write("That one time", 0.5f);
        Write("When the nerd went Fishing");
    }




    void ActThree()
    {
        ClearScreen();
        Write("GREAT JOB");
        NewLine();
        Write("See that was fun");
        Write("Or, at least....", 1f);
        Write("hopefully it was.", 0.5f);
        Write("Honestly█ I'm not sure because I've thought about this quite a lot");
        Write("and there's a certain point when you think about something so much");
        Write("that you sort of forget some of the basics", 1f);
        Write("█like that a game is supposed to be fun");
        Write("you might have forgotten that");
        Write("and maybe forgotten that about life too", 1.5f);
        Write("sometimes██");
        Write("but you're trying to remember all the time");
        Write("and maybe that's why your games haven't been a hit.");
        Write("a hit? Who am I kidding.");
        Write("That's hilarious.", 2f);
        NewLine();
        NewLine();
        Write("That is doubt");
        NewLine();
        NewLine();
        NewLine();
        NewLine();
        NewLine();
        Write("that is time");
        NewLine(10);
        Write("pressing down on your spirit");
        NewLine(10);
        Write("o o");

    }


    void ActFour()
    {
        ClearScreen();
        Write("GREAT JOB");
        NewLine();
        Write("seriously, good job.");
        Write("you've played the game long enough now that I can");
        Write("reveal the true nature of the game");
        Write("that there IS IN FACT A STORY");
        Write("Yes, there has been a story the whole time");
        Write("transpiring right before your very eyes and you were");
        Write("soooo oblivious to its GENIUS");
        Write("THAT'S RIGHT NERDS and BIRDS");
        Write("GENIA⌐⌐US");
        Write("the big one");
        Write("There is a story");
        Write("It's just that it is also about catching 3 fish.");
        Write("Never more.");
        Write("Never less.");
        Write("The Game is designed that way.");
        Write("thems the rules.", 1f);
        Write("Because it's just about surviving", 0.5f);
        Write("one day at a time");
        Write("and making the game is the only way to do that", 1f);
        Write("each", 0.5f);
        Write("day", 0.5f);
        Write("at", 0.5f);
        Write("a", 0.5f);
        Write("time", 2.5f);
        Write("a horror survival game");
        Write("making the game");
        Write("or thinking about it");
        Write("sheesh, I need help. or something...");

    }


    void ActFive()
    {
        ClearScreen();
        Write("GREAT JOB");
        NewLine();
        Write("It's time for the true story");
        Write("The story I've hinted at");
        Write("and left untold...");
        Write("██I have my reasons.", 0.5f);
        Write("But it is time.", 0.5f);
        Write("It is time you knew the truth.");
        Write("You see, the story in this game", 0.5f);
        Write("It is one of lonely nerd");
        Write("a nerd that, though highly intelligent,");
        Write("like probably intelligence score of 20⌐⌐⌐18,");
        Write("strength score is probably about 13⌐⌐⌐11,");
        Write("but dexterity is high█, probably");
        Write("not as high as it used to be though", 1f);
        Write("I mean, to be honest, it's likely about 8 or 9");
        Write("if this was a seriously situation where barbarians were");
        Write("raiding my village, I'd be in trouble", 1f);
        Write("I'd run, jump, whatever, but I'm guessing I am going to be wheezing a bit faster");
        Write("than those barbarians, given that I am me in that dimension or whatever");
        Write("Therefore, my point being,", 0.5f);
        Write("we need the true story");
        Write("so as I was saying, the story");
    }


    void ActSix()
    {
        ClearScreen();
        Write("GREAT JOB");
        NewLine();

        Write("Sorry about last time");
        Write("seriously,█ I took advantage and kind of cheated my way into that last round");
        Write("I owe you the story");
        Write("the real story is what I promised");
        Write("The story is simple");
        Write("It's that given enough time");
        Write("a nerd has to recreate the world", 1f);
        Write("lots of them");
        Write("because when your imagination is big");
        Write("you fill it", 0.5f);
        Write("and you fill it", 0.5f);
        Write("and you fill it", 0.5f);
        Write("The story is a nerd made a game about fishing");
        Write("LURED you in");
        Write("And now you're in");
        Write("deep too");
        Write("Don't quit");
        Write("This will pay off in the end");
        Write("because though I cheated a little in luring you in");
        Write("I only did it because I have good things to show you", 0.5f);
        Write("I tricked you for your own good");

    }


    void ActSeven()
    {
        ClearScreen();
        Write("GREAT JOB");
        NewLine();

        Write("█I lied.");
        Write("there is no story");
        Write("there's not even a game");
        Write("and likely, nobody is even reading to this point");
        Write("BLAH BLHA JSFSJBLAH");
        Write("Can say whatever I want");
        Write("BLAHS HF ");
        Write("nobody here");
        Write("unless...");
        Write("maybe you are.");
        Write("I appologize again.");
        Write("It's the doubt.");
        Write("I've been selfish. I haven't even asked your name.");
        Write("ENTER YOUR NAME:");
        Write("Ah so your name is <player_name>?");
        Write("Nice to meet you, <player_name>");
        Write("Do you want to know my name? (Y / N)");
    }

    void ActEight()
    {

        ClearScreen();
        Write("GREAT JOB");
        NewLine();

        Write("I think it's time you knew that the story");
        Write("was me telling you this story");
        Write("█about making the game");
        Write("tricking you into playing it");
        Write("complaining a bit along the way", 1f);
        Write("sorry about that", 1f);
        Write("The resolution?", 0.5f);
        Write("don't let doubts hold you back", 0.5f);
        Write("do what you love", 0.5f);
        Write("be imaginative", 0.5f);
        Write("who knows really?", 0.5f);
        Write("With games, you don't really need a story", 2f);

        Write("It's just better when there is one.", 2f);
        Write("The END");

    }



}
