namespace org.bizzle.pathfinding

  type pathingStatus =
    | Continue of sd: stepData
    | Success  of sd: stepData
    | Failure  of sd: stepData
