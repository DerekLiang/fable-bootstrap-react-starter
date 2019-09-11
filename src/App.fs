module App

open Fable.Core
open Fable.React
open Fable.React.Props
open Fable.Core.JsInterop

module Progress =
    type ProgressProps =
      | Percent of int
      | StrokeWidth of int
      | StrokeColor of string

    [<StringEnum>]
    type Size =
      | Small
      | Normal
      | Big

    let inline progressLine (props : ProgressProps list) (elems : ReactElement list) : ReactElement =
        ofImport "Line" "rc-progress" (keyValueList CaseRules.LowerFirst props) elems
module kendoReact =

    type NumericTextBoxProps =
        | Label of string
        | Mask of string


    let inline numericTextBox (props : NumericTextBoxProps list) (elems : ReactElement list) : ReactElement =
        ofImport "Input" "@progress/kendo-react-inputs" (keyValueList CaseRules.LowerFirst props) elems

    let inline maskedTextBox (props : NumericTextBoxProps list) (elems : ReactElement list) : ReactElement =
        ofImport "MaskedTextBox" "@progress/kendo-react-inputs" (keyValueList CaseRules.LowerFirst props) elems

open Elmish
open Elmish.React
open Feliz

type State =
    { Count: int }

type Msg =
    | Increment
    | Decrement

let init() =
    { Count = 0 }

let update (msg: Msg) (state: State): State =
    match msg with
    | Increment ->
        { state with Count = state.Count + 1 }

    | Decrement ->
        { state with Count = state.Count - 1 }


open Reactstrap
importSideEffects "../node_modules/bootstrap/scss/bootstrap.scss"

let private alertSample =
    FunctionComponent.Of<obj>
        ((fun _ ->
            fragment []
                [ Alert.alert [ Alert.Color Primary ] [ str "This is a primary alert — check it out!" ]
                  Alert.alert [ Alert.Color Info ] [ str "This is a secondary alert — check it out!" ]
                  Alert.alert [ Alert.Color Success ] [ str "This is a success alert — check it out!" ]
                  Alert.alert [ Alert.Color Danger ] [ str "This is a danger alert — check it out!" ]
                  Alert.alert [ Alert.Color Warning ] [ str "This is a warning alert — check it out!" ]
                  Alert.alert [ Alert.Color Info ] [ str "This is a info alert — check it out!" ]
                  Alert.alert [ Alert.Color Light ] [ str "This is a light alert — check it out!" ]
                  Alert.alert [ Alert.Color Dark ] [ str "This is a dark alert — check it out!" ] ]), "AlertSample")

let render (state: State) (dispatch: Msg -> unit) =
  Html.div [
    Html.button [
      prop.onClick (fun _ -> dispatch Increment)
      prop.text "Increment"
    ]

    Html.button [
      prop.onClick (fun _ -> dispatch Decrement)
      prop.text "Decrement"
    ]

    Html.h1 state.Count

    Progress.progressLine [ Progress.Percent state.Count ] []

    kendoReact.maskedTextBox [ kendoReact.Mask "(999) 000-00-00-00"] []


    Container.container []
              [ Row.row []
                    [ Col.col [ ] [ alertSample() ]
                    ] ]
  ]



Program.mkSimple init update render
|> Program.withReactSynchronous "elmish-app"
|> Program.run