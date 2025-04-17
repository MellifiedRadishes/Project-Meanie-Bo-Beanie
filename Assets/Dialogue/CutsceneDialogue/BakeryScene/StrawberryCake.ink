INCLUDE ../../Global.ink
INCLUDE BakerChallenge.ink

~ cake_flavor = "strawberry"
~ ChangeSpeaker("SCOUT")
Ooh, {cake_flavor}! Do you think we should get that one?
   * Yes
   -> BakerChallenge
   * No
    Okay, maybe not, then.
    It’s… kind of a binary choice, though. Don’t think too hard about it!
    -> END

