﻿// Learn more about F# at http://fsharp.org
module Program
module V = col.Vector
type intVector = col.Vector<int>
module SV = col.Vector.SubVector

  
     
let smallData () =
    let emptyInt = intVector.EMPTY
    let a = 
      intVector.EMPTY 
      |> V.add 11 
      |> V.add 22
    let b = 
      intVector.EMPTY
      |> V.add 11 
      |> V.add 22
    let c = 
      intVector.EMPTY
      |> V.add 11 
      |> V.add 33
    let sa = V.sub 0 2 a
             |> Option.get 
    let ra = V.rev a
    struct {|a= a; b= b; c= c; sa = sa; ra = ra|}

let tryList () =
    let (a,b,c) = let data = smallData () 
                  (data.a, data.b, data.c)
    if  (V.toList a) = [11;22] |> not then failwith "Not added"

let tryEquals () =
    let (a,b,c) = let data = smallData () 
                  (data.a, data.b, data.c)
    
    if not (a = b)
    then failwith "not equal"
    if a = c 
    then failwith "Should not be equal"

let tryFirst () =
    let a= (smallData ()).a 
    if not (Some 11 = (V.first a)) then failwith "not first"
   
let trysize () =
    let a= (smallData ()).a 
    if not (V.size a = 2) then failwith "not good size"
    if not (V.size (V.empty ()) = 0) then failwith "not good size"

let tryget () =
    let a= (smallData ()).a 
    if not (Some 22 = (V.get 1 a)) then failwith "Not second" 
    if not  (Some 11 = (V.get 0 a)) then failwith "Not First"
    if not (None = (V.get -1 a)) then failwith "No outofbounds"
    if not (None = (V.get 2 a)) then failwith "No outof upper bounds"
    
let tryAddAllAndAppend ()=
    let (a,b,c) = let data = smallData () 
                  (data.a, data.b, data.c)
    let ac = 
         intVector.EMPTY
         |> V.add 11 
         |> V.add 22
         |> V.add 11 
         |> V.add 33
    if not (ac = V.append c a) 
    then failwith "not appended"
    if not (ac = V.addAll [11;33] a)
    then failwith "not addAlled"
    if not ( (V.empty ()) = V.append (V.empty ()) (V.empty ()) )
    then failwith "no empty append"
    if not (a = V.addAll [] a)
    then failwith "not addAll empty"
    

let tryOfList () =
    let a= (smallData ()).a 
    if not (a = V.ofList [11;22])
    then failwith "not ofList"
    if not ((V.empty ()) = V.ofList [])
    then failwith "no empty list"

let trySet () =
    let (a,b,c) = let data = smallData () 
                  (data.a, data.b, data.c)
    if not (Some c = V.set 1 33 a)
    then failwith "Could not set"
    let x = V.add 1 (V.empty ())
    let y = (V.set 0 1 (V.empty ())).Value 
    if not (x = y)  
    then failwith "set on empty"
    if not (None = (V.set 1 1 (V.empty ())))
    then failwith "Setting out of bounds"
    if not (None = (V.set -1 1 (V.empty ())))
    then failwith "Setting out of bounds"
    

let tryDrop () =
    let a= (smallData ()).a
    if not (Some (V.empty () |> V.add 11) = V.drop 1 a)
    then failwith "no drop"
    if not (Some (V.empty () ) = V.drop 2 a)
    then failwith "no drop all"
    if not (None = V.drop 3 a)
    then failwith "no drop too much"

let tryLast ()=
    let a= (smallData ()).a
    if not (Some 22 = V.last a)
    then failwith "Not last"
    if not (None = V.last (V.empty ()))
    then failwith "Not last empty"

let tryFold ()=
    let a= (smallData ()).a
    if not (33 = V.fold (+) 0 a)
    then failwith "not folding sum"
    if not (0 = V.fold (+) 0 (V.empty ()))
    then failwith "not folding empty"

let tryFilter ()=
    let a= (smallData ()).a
    if not ( (V.ofList [11]) = (V.filter (fun x -> x % 2 = 1) a))
    then failwith "not filtered"
    if not (V.empty() = V.filter  (fun x -> x % 2 = 1) (V.empty ()))
    then failwith "not nothing filter"

let tryCut ()=
  let a= (smallData ()).a
  if not ((V.cut 1 a |> function Some v -> SV.toList v
                               | None -> failwith "was none") = [11])
  then failwith "no cut"
  if not (None = (V.cut 1 (V.empty ()))) 
  then failwith "emptyCut"

  if not (None = (V.cut -1 (V.empty ()))) 
  then failwith "emptyCut"

  if not (None = (V.cut -1 a)) 
  then failwith "emptyCut"
  if not ((V.cut 0 (V.empty ()) |> function Some v -> SV.toList v
                                          | None -> failwith "was none") = [])
  then failwith "no cut 0"


let tryOfSub ()=
    let a= (smallData ()).a
    if not ((=) (V.empty ()
                 |> V.add 22
                 |> V.add 11)
                (V.rev a
                 |> V.ofSub))
    then failwith "no of Sub"
    let x = V.empty ()
    let y = V.empty () 
            |> (V.sub 0 0)
            |> Option.get
            |> V.ofSub
    if not (x=y)
    then failwith "no empty of Sub"
    if not (None = V.sub -1 2 a)
    then failwith "No lower bounds on sub"
    if not (None = V.sub 10 1 a)
    then failwith "No high bounds on sub"
    if not (None = V.sub 0 3 a)
    then failwith "no upper bounds on sub"
    
let subList () =
    let (a,sa) = let data = smallData () 
                 (data.a, data.sa)
    if not (SV.toList sa = V.toList a)
    then failwith "lists are not equal"
    if not ([] = (V.empty () |> V.rev |> SV.toList))
    then failwith "no empty sublist"
    
let trySubGet () = 
    let (a,sa,ra) = let data = smallData () 
                    (data.a, data.sa, data.ra)
    if not (SV.get 0 sa = V.get 0 a)
    then failwith "first is not equal"
    if not (SV.get 2 sa = V.get 2 a)
    then failwith "first is not equal"
    if not (None = SV.get 2 sa)
    then failwith "first outofbounds"
    if not (None = SV.get -1 sa)
    then failwith "first outofbounds"
    if not (Some 11 = SV.get 0 sa)
    then failwith "not the first"
    if not (Some 11 = SV.get 1 ra)
    then failwith "no get on rev"
    if not (None = SV.get 2 ra)
    then failwith "outofbounds"
    if not (None = SV.get -1 ra)
    then failwith "less than zero rev"
    if not (Some 11 = SV.first sa)
    then failwith "not the first"

let tryFirstRev ()=
    let (sa,ra) = let data = smallData () 
                  (data.sa, data.ra)
    if not (sa = SV.rev ra)
    then failwith "no rev rev"
    if not (Some 22 = SV.first ra)
    then failwith "no get on rev"
    if not (Some 11 = SV.last ra)
    then failwith "last ra"
    if not (SV.first sa = SV.last ra)
    then failwith "last first"

let trySubDrops ()=
    let (sa,ra) = let data = smallData () 
                  (data.sa, data.ra)
    if not (Some 22 =(SV.drop 1 ra 
                      |> function Some v -> SV.last v 
                                | None -> failwith "none"))
    then failwith "no reverse drops"
    if not (Some 11 =(SV.drop 1 sa 
                         |> function Some v -> SV.last v 
                                   | None -> failwith "none"))
     then failwith "no forward drops"
let trySubCuts () =
    let (sa,ra) = let data = smallData () 
                  (data.sa, data.ra)
    let h1 = intVector.EMPTY  |> V.add 11 |> V.sub 0 1 
    let h2 = SV.cut 1 ra
    if not ( h1 = h2 )
    then failwith "not reverse cuts"
  
    if not (V.empty () |> V.add 22 |> V.sub 0 1 = SV.cut 1 sa )
    then failwith "no cuts"
let trySubSubs () =
    let (sa,a) = let data = smallData () 
                 (data.sa, data.a)
    if not ((V.sub 1 1 a) = (SV.sub 1 1 sa))
    then failwith "no subsub"
    let x = V.ofList [3;2;1] |> V.sub 0 3
    let y1 =  V.rev (V.ofList [1;2;3;4])
    let y = SV.sub 1 3 y1
    if not (x = y)
    then failwith "no reverse sub"

let trySubFolds () =
    let (sa,ra) = let data = smallData () 
                  (data.sa, data.ra)
    if not ([22;11] = SV.fold (fun a x -> x::a) [] sa)
    then failwith "not folding sum"
    
    if not ([11;22] = SV.fold (fun a x -> x::a) [] ra)
    then failwith "not folding sum"

    if not ([11;22] = SV.foldBack (fun x a -> x::a) sa [])
    then failwith "not folding sum"
    
    if not ([22;11] = SV.foldBack (fun x a -> x::a) ra [])
    then failwith "not folding sum"

let tryRevReving () =
    let (sa,ra) = let data = smallData () 
                  (data.sa, data.ra)
    if not (sa = SV.rev ra)
    then failwith "not reving the rev"

let trySubListing ()=
    let (sa,ra) = let data = smallData () 
                  (data.sa, data.ra)
    if not ([11;22] = SV.toList sa)
    then failwith "not listing sub"

    if not ([22;11] = SV.toList ra)
    then failwith "not listing rev"

let tryBind () =
    let a = V.ofList [1;2;3]
    let x = V.ofList [0;1;0;2;0;3]
    let y = V.bind (fun x -> V.ofList [0;x] ) a
    if not (x = y)
    then failwith "no bind"

let mutable (data:Map<string, int64 list>)  = Map.empty
let duration str f = 
    let timer = new System.Diagnostics.Stopwatch()
    timer.Start()
    let returnValue = f()
    timer.Stop ()
    match Map.tryFind str data with
    | Some l -> data <- Map.add  str ((timer.ElapsedMilliseconds)::l) data
    | None -> data <- Map.add  str [timer.ElapsedMilliseconds] data
   
    returnValue  

let performanceCopyOnWrite n = 
   duration (sprintf "Add copyOnWrite %i" n) 
                    (fun _ -> List.fold (fun (a:System.Collections.Generic.List<int>) v ->
                                             let b =System.Collections.Generic.List<int> a
                                             b.Add v |> ignore
                                             b) 
                                         ( System.Collections.Generic.List<int> ()) 
                                         [0..n]) |> ignore
let performance n =
   
   for y in [0..2] do
      printfn "%i %i" n y 
      let r = duration  (sprintf "Add %i" n)  
                        (fun _ -> List.fold (fun a v -> V.add v a) (V.empty ()) [0..n])
      duration  (sprintf "Add linked list %i" n)  
                        (fun _ -> List.fold (fun a v -> v::a) ([]) [0..n]) |> ignore
      duration  (sprintf "Add linked reverse %i" n)  
                   (fun _ -> List.fold (fun a v -> v::a) ([]) [0..n]) |> List.rev |> ignore
      let al = duration (sprintf "Add Generic List %i" n) 
                        (fun _ -> List.fold (fun (a:System.Collections.Generic.List<int>) v -> 
                                                a.Add v |> ignore
                                                a) 
                                            ( System.Collections.Generic.List<int> ()) 
                                            [0..n])

      duration (sprintf "Get %i" n) 
               (fun x -> List.iter (fun x -> if not (Some x = V.get x r) 
                                             then failwith "unexpected when indexing bigger") 
                                   [0..n])
      duration (sprintf "Get Generic List %i" n)
               (fun x -> List.iter (fun (x:int) -> if not (Some x = Some (al.[x]) ) 
                                                   then failwith "unexpected when indexing bigger") 
                                   [0..n])
      let r = duration (sprintf "Set %i" n) 
                       (fun x -> List.fold (fun a x -> (let inc = 1 + (V.get x r).Value
                                                       V.set x inc a).Value) 
                                           r 
                                           [0..n])
      let al = duration ( sprintf "Set Generic list %i" n) 
                        (fun x -> List.fold (fun (a:System.Collections.Generic.List<int>) x -> 
                                                  let inc = 1 + (Some (al.[x])).Value
                                                  a.[x] <- inc
                                                  (Some a).Value ) 
                                            al 
                                            [0..n])
      
      List.iter (fun x -> if not (Some (x+1) = V.get x r) then failwith "unexpected when modifying bigger") [0..n]

      let n32 = List.fold (fun a v -> V.add v a) (V.empty ()) [1..32]
      let n15 = List.fold (fun a v -> V.add v a) (V.empty ()) [1..15]
      let appended = duration (sprintf "append to 32 %i" n)  
                              (fun _ -> V.append r n32)
      let appended = duration (sprintf "append to 15 %i" n)  
                              (fun _ -> V.append r n15)
      let appended = duration (sprintf "old append to empty %i" n) 
                              (fun _ -> V.oldAppend r (V.empty ()))
      let l32 = List.fold (fun (a:System.Collections.Generic.List<int>) v -> 
                                      a.Add v
                                      a) 
                          (System.Collections.Generic.List<int> ()) [1..32]
      duration (sprintf "append to 32 list %i" n) 
               (fun _ -> (l32).AddRange al)

      duration (sprintf "fold %i" n) (fun () -> V.fold (+) 0 r) |> ignore
      duration (sprintf "fold subvec %i" n) (fun () -> SV.fold (+) 0 (V.sub 0 (V.size r) r).Value) |> ignore
       

   let r = List.fold (fun a v -> V.add v a) (V.empty ()) [0..1056] 
   let r = List.fold (fun a v -> (V.pop a).Value) r [0..1056]
   if not (r=V.empty ()) then failwith "not emptied"

let printData () =
  Map.iter (fun k v -> List.sort v
                       |> List.item (((List.length v)/2)) 
                       |> printfn "%s:%i" k) 
                       data
let tryFoldUntil () =
    let a= (smallData ()).a
    if not ((33, None) = ( V.foldUntil (fun a v -> (a+v, None)) 0 a))
    then failwith "not fold until all the way"
    if not ((11, Some "Hi") = ( V.foldUntil (fun a v -> (a+v, Some "Hi")) 0 a))
    then failwith "not fold until once"
    if not ((22, Some "Hi") = ( SV.foldUntil (fun a v -> (a+v, Some "Hi")) 0 (V.rev a)))
    then failwith "not fold until once backwards"
    if not ((33, None) = ( SV.foldUntil (fun a v -> (a+v, None)) 0 (V.rev a)))
    then failwith "not fold until all backwards"

let tryFoldWhile () =
    let a= (smallData ()).a
    if not (33 = ( V.foldWhile (fun a v -> (a+v, true)) 0 a))
    then failwith "not fold until all the way"
    if not (11 = ( V.foldWhile (fun a v -> (a+v, false)) 0 a))
    then failwith "not fold until once"
    if not (22 = ( SV.foldWhile (fun a v -> (a+v, false)) 0 (V.rev a)))
    then failwith "not fold until once rev"
    if not (33 = ( SV.foldWhile (fun a v -> (a+v, true)) 0 (V.rev a)))
    then failwith "not fold until all rev"


let loo () =
    tryList ()
    tryEquals ()
    tryFirst ()
    trysize ()
    tryget ()
    tryAddAllAndAppend ()
    tryOfList ()
    trySet ()
    tryDrop ()
    tryLast ()
    tryFold ()
    tryFilter ()
    tryCut ()
    tryOfSub ()
    trySubGet ()
    tryFirstRev ()
    trySubDrops ()
    trySubCuts ()
    trySubSubs ()
    trySubFolds ()
    tryRevReving ()
    trySubListing ()
    tryBind ()
    performance 10000
    performance 100000
    performance 500000
    performanceCopyOnWrite 10000
    tryFoldUntil ()
    tryFoldWhile ()
    printData ()
    printfn "The skinny goat says all is nice and shiny"
 //   printfn "%A" (V.leafpaths 250)
   // let x = List.fold (fun a v -> V.add v a) (V.empty ()) [0..120] 
  //  printfn "%A" (V.sequence x |> Seq.toList)
   // printfn "%A" (V.arrays x |> Seq.toList)

    

[<EntryPoint>]
let main argv = 
  for i in 0..0 do
    loo ()
  0
