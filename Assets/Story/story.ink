VAR hasQuest = false
VAR knowsBigFish = false
VAR hasRing = false

-> menu

== menu ==

// comment out this done to test in the inky editor
//-> DONE

+ [Tom] -> Tom
+ [Edna] -> Edna
+ [Joe] -> Joe
+ [Ralph] -> Ralph
+ [Sam] -> Sam
+ [Walter] -> Walter
+ [Find Fish] -> FindFish

== Tom ==

The whole town was flooded months ago.
We had no where else to go but up on the roofs.
If you're looking for fish there are plenty out there.
Between the bags of trash and flooded homes.
But I don't know if I would trust eating them.

-> menu

== Edna ==

{ hasRing: -> Edna3 }
{ hasQuest: -> Edna2 }
-> Edna1

== Edna1 ==

Hello there, fisher.
I lost my ring.
A big koi fish swallowed it.
We don't have much left, just a few rooftops to live on.
It would mean so much to me to find that ring.
~ hasQuest = true

-> menu

== Edna2 ==

A big koi fish swallowed my ring.
Won't you find it?

-> menu

== Joe ==

{ knowsBigFish: -> Joe2 }
-> Joe1

== Joe1 ==

A big fish?
I saw the one you're looking for over by the old shrine.
I remember when I was young we would pass under the red gate.
That was before the world changed.
~ knowsBigFish = true

-> menu

== Joe2 ==

I last saw the big fish over by the shrine, by the red gate.

-> menu

== Edna3 ==

My ring!
Tell me, how did you find it?
...
Is that so? How wonderful.
It is one of the few possessions I have left in this world.
Thank you.

-> menu

== Ralph ==

-> menu

== Sam ==

-> menu

== Walter ==

-> menu

== FindFish ==

You found the koi fish!
A ring is inside its mouth.
This must be Edna's.
~ hasRing = true

-> menu