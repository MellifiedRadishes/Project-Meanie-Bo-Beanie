EXTERNAL ChangeSpeaker(name)
EXTERNAL PlayCutscene(cutscene)
EXTERNAL ChangeStoryPoint(storypoint)
EXTERNAL CombatTransition()
EXTERNAL LoadScene(sceneIndex)
EXTERNAL UpdateDialogueState(state)
EXTERNAL UpdateCakeFlavor(flavor)

VAR cake_flavor = "chocolate"
VAR dialogue_state = 0
VAR current_story_point = 0

=== function ChangeSpeaker(name) ===
// Placeholder
~ return 1

=== function PlayCutscene(cutsceneIndex) ===
// Placeholder
~ return 1

=== function ChangeStoryPoint(storypointIndex) ===
~ current_story_point = storypointIndex

=== function CombatTransition() ===
~ return 1
=== function LoadScene(sceneIndex) ===
~ return 1
=== function UpdateDialogueState(state)===
~ return 1

=== function UpdateCakeFlavor(state)===
~ cake_flavor = "chocolate"