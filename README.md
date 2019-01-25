# Summoner's Quest

## Who did this project?

* __Inês Gonçalves__
  * a21702076

* __Inês Nunes__
  * a21702520

## Git repository

We worked in a private repository that will be available publicly in this
[link](https://github.com/FiammaVolts/SummonersQuest) after the deadline.

## Task distribution

The two of us have worked equally as much, both online and next to each other.
Below is a detailed distribution.

* __Inês Gonçalves__
  * Enumerations: InteractibleType, NPCState
  * Classes: CanvasManager, Credits, Interactible, MainMenu, NPCAction, PauseMenu, Player, StateManager, SwitchSounds;
  * Made the UML diagram;
  * Helped with the comments;
  * Helped with the XML comments;
  * Helped with the report.

* __Inês Nunes__
  * Enumerations: InteractibleType;
  * Class: CanvasManager, Credits, Interactible, MainMenu, NPCAction, PauseMenu, Player, StateManager;
  * Added comments;
  * Added XML comments;
  * Helped with UML diagram;
  * Made the report.


## Our solution

### Architecture

The program was organized using _classes_ and _enumerations_ for easier
understanding and to keep the code cleaner.

We have _Lists_ and a _State manager_ to help deal with the _NPCs_ and _Interactibles_.
Most _classes_ have a _Start_ and _Update_ method.

We have three _scenes_ that once the game starts, will be called accordingly: Menu,
the one containing the game, and the Credits.
We also have an audio source that will play when the player crosses certain areas, and will
change every time.


### UML Diagram

![UML Diagram](https://i.imgur.com/8ix7AkQ.png)


## Conclusions

With this project we learned how to better use Unity and its functions.
We also learned how lambdas work and how to use them.


## References

* <a name="ref1">[1]</a> Whitaker, R. B. (2016). The C# Player's Guide
  (3rd Edition). Starbound Software.

* [Unity API](https://docs.unity3d.com/Manual/index.html)
