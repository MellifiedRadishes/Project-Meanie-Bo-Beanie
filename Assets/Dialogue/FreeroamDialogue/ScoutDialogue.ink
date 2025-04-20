INCLUDE ../Global.ink

{ current_story_point:

    - 1:
        -> pre_doenuts
    - 2:
        -> post_doenuts
    - 4:
        -> post_murder
    - else:
        If you can read this, something has gone terribly wrong
        -> END
}
-> END
====pre_doenuts====
~ ChangeSpeaker("SCOUT")
Let’s head to Doenuts. Hopefully Buck’ll cut us a deal…
-> END

====post_doenuts====
~ ChangeSpeaker("SCOUT")
You… do know where Wally lives, right? He’s your next-door neighbor!
-> END

====post_murder====
~ ChangeSpeaker("SCOUT")
We just need to get through the forest…
-> END