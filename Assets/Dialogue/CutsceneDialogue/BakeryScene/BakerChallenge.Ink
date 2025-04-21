==== BakerChallenge ====
~ ChangeSpeaker("SCOUT")
Alright, sounds good!
Hey, Buck! How much for this one?
~ ChangeSpeaker("BUCK")
Excellent choice! That’ll be sixteen dollars.
~ ChangeSpeaker("SCOUT")
Ah.
Okay.
Meanie, could you, uh… spot me sixteen dollars
    * "No"
    Alright, guess we’re switching to plan B.
    -> divert1
    * “I’m a CM major”
    real.
    -> divert1

==== divert1 ====
How does two dollars and a favor sound, Buck?
~ ChangeSpeaker("BUCK")
Sounds like you’re not bringing home a cake.
~ ChangeSpeaker("SCOUT")
But it’s for Walnut’s birthday party! You know Wally, right?
It wouldn’t be a birthday without cake!
~ ChangeSpeaker("BUCK")
…Tell you what: I’ve been taking aikido lessons for about a week and a half now. 
Sensei says I have to practice my pins.
Let’s have a friendly spar! If you win, I’ll give you the cake.
~ ChangeSpeaker("SCOUT")
Meanie, we should go for it! This might be the only way to get a cake for Wally!
You take him on, and I’ll be moral support. How’s about it?
    * "That’s not fair"
    The next time somebody has a birthday and we have to fight the baker for a cake, I’ll step in the ring. 
    Deal? Deal.
    -> divert2
    *"This’ll be so cathartic"
    That’s the spirit! Beat his ass!
    -> divert2
====divert2====
~ ChangeSpeaker("BUCK")
Alright, let’s get to it!
* Begin Combat
    ~ ChangeStoryPoint(2)
    ~ CombatTransition()
    -> END
