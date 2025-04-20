INCLUDE ../../Global.ink
~ PlayCutscene("IntroKnockScene")
~ ChangeSpeaker("SCOUT")
- Meanie? Are you up yet?
~ PlayCutscene("WakeUpScene")
- C’mon, Meanie! We’ve got Wally’s birthday party today, remember?
~ ChangeSpeaker("")
- Meanie needs your help to make decisions! What should she say?
*   "I totally forgot!"
    ~ ChangeSpeaker("SCOUT")
    Jeez, Meanie! Really? 
    Well, it’s a good thing you’ve got me here to remember for you.
    -> end_convo
*   "I thought that was last year!"
    ~ ChangeSpeaker("SCOUT")
    Uh, they’re kind of an annual thing. Like Halloween, but with less pumpkins.
    -> end_convo
=== end_convo ===
Meet me outside when you’re ready, okay?
Just… y’know, walk down that hallway. And open the door. Like you’ve done a billion times before.
Nice and simple, right?
I’ll be waiting!
~ ChangeSpeaker("")
~ ChangeStoryPoint(1)
Press WASD to move and F to interact with objects!
-> END

