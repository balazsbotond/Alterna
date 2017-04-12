// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake

// Targets
Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

// start build
RunTargetOrDefault "Default"