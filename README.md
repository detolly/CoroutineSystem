# CoroutineSystem
Coroutines with Unity Syntax in Normal C#.

Needs manual edit in Program file. Form1 is an example of what you could do with it.

Program needs to implement the static function
```cs
CoroutineManager.Init();
```
as well as

```cs
CoroutineManager.Interval();
```

Examples have been shown.
___
## Some Documentation
___

### Init

The static function ```Init``` sets up all the lists that are used in the Manager itself. Although this could have been done on the first iteration cycle, this secures that memory is being used efficiently and that the CPU goes through roughly the same amount of junk every cycle.

More specifically, the private data that is held within the CoroutineManager is as follows:

```cs
private static DateTime lastFrame;
private static List<IEnumerator> currentCoroutines;
private static List<IEnumerator> endlessCoroutines;
private static List<IEnumerator> lateCoroutines;
private static List<IEnumerator> toRemove;
private static List<Tuple<IEnumerator, IEnumerator>> listOfAfter;
```

### Interval

The static function ```Interval``` goes through the coroutines as one frame, of course you can do this however often you want (as specified in the main thread loop). The function will go through the private List of coroutines and execute them. If specified waittime, this will use DateTime instead of multithreading. This is to simulate the feeling of asynchrounous behavior, despite running on one Thread.

For forkers: This can be heavily improved. The function goes through every coroutine one by one, instead of having a dedicated list of coroutines that HAVE to be run, aka the ones that are not on wait. Implementing this feature will have a big impact on the efficiency of the system. Another way of doing this could be to have a sorted array that keeps track of which coroutines are on wait, and performing binary search.
