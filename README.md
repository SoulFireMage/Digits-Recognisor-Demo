Dojo "Digits Recognizer"
======================

This is an exercise that I've used to get into how F# works in more depth. At time of writing there is no async/parallel work done at all and, yes, there is an index bug at 500 samples (Just change dataSet = [| 0 .. 500 |].

I've played with the function aspect as I'd like to create a function generator that evolves new int->int->float functions against the cost of accuracy/time. Hence there is a timer as well.

So far it's 36 lines of code ignoring comments & blanks and I get 96.07843137% accuracy.

This Dojo is inspired by the [Kaggle Digit Recognizer contest](http://www.kaggle.com/c/digit-recognizer).
The goal is to write a **Machine Learning** classifier from scratch, a program that will recognize hand-written digits automatically.
It is a guided Dojo, **suitable for beginners**: the Dojo comes with a Script file, with specific tasks to complete, introducing along the way numerous F# concepts and syntax examples.
The Dojo is on the longer side; for a group with limited F# experience, it is recommended to **schedule it for 2 hours and a half**.
