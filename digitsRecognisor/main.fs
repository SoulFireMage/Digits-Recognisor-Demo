open System
open System.IO
open System.Diagnostics

//Simple digits recognisor - a Kaggle dojo solution that yields 94.06% over 100 samples, 500 samples gives an index issue to be sorted out. Now we can just try other types of calculation!!
//What if we could use a genetic algorithm to generate other functions rather than Euclidean distance and tries them out?

let trainingFile = __SOURCE_DIRECTORY__ + @"\trainingsample.csv"
let validationFile = __SOURCE_DIRECTORY__ + @"\validationsample.csv"
 
type Example = { Label:int; Pixels:int[] }
let getData file  =
     File.ReadAllLines(file).[1 ..]
         |> Array.map(fun x -> x.Split(',') |> Array.map(fun s -> (int) s)) 
         |> Array.mapi(fun i x -> {Label = i; Pixels = x})
                  
let trainingExamples,validationExamples = getData trainingFile, getData validationFile 

//calcFn is the testing function
let distance (points1:int[],points2:int[], calcFn : int->int->float) = 
                Array.map2(calcFn) points1 points2 // calcFn here could be injected into this via a gene expression algorithm that tries to generate better functions
                |> Array.sum |> sqrt
 
let findMinimumPixels s sample calcFn = 
    sample  
    |> Array.map(fun x -> (x.Label,  x.Pixels), distance (x.Pixels , s.Pixels, calcFn ))
    |> Array.minBy(fun x -> snd x) |> fun x -> (fst x |> fun a -> snd(a).[0]) , s.Pixels.[0] 

let classify (unknown:int[]) calcFn =
    unknown |> Array.map(fun i-> validationExamples.[i] |> fun s -> findMinimumPixels s trainingExamples calcFn) |> Array.map(fun x -> fst x, snd x)
let dataSet = [|0 .. 500|]  

let results data calcFn = classify data calcFn
                          |> Array.map(fun x -> match x with 
                                                | n, m when n = m -> 1 
                                                | _,_ -> 0) 
                          |> Array.sumBy(fun x -> float (x) / float (data.Length) * 100. )  

let timedResult cmd =
    let timer = Stopwatch.StartNew()  
    cmd 
    timer.Stop() 
    printfn "Milliseconds it took me: %f" timer.Elapsed.TotalMilliseconds 
    timer.Reset

//All I'm varying here is the input function that will then be summed and square root found. The first function is the Euclidean distance 
let functions  = [fun p1 p2 -> (float p1  -  float p2)**2.;  
                  fun p1 p2 -> ((float p1  -  float p2)/2.)**3.;  
                  fun p1 p2 -> sqrt(float p1 - float p2) ** (0.33333)]     
for i in functions do
      timedResult (printfn "%A  accuracy achieved" (results dataSet i)) |> ignore
Console.ReadLine()
 
 