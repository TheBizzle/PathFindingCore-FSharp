namespace org.bizzle.pathfinding

open org.bizzle.pathfinding.coordinate
open org.bizzle.pathfinding.pathingmap

  type stepData =
    | StepData of
        loc:           coordinate     *
        endCoord:      coordinate     *
        pathingMap:    pathingMap     *
        breadcrumbArr: coordinate [,] *
        endGoal:       coordinate

