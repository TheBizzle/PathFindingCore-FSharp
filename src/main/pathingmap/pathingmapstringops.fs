namespace org.bizzle.pathfinding.pathingmap

open org.bizzle.pathfinding.coordinate

  type pathingMapString =
    | PathingMapString of
        str:   string *
        delim: string

  type pathingMapData =
    | PathingMapData of
        start: coordinate *
        goal:  coordinate *
        cols:  int        *
        rows:  int        *
        arr:   terrain [,]

  module PathingMapStringOps =

    let convertArray (cols: int) (rows: int) (inArr: string array) =
      let outArr = Array2D.create cols rows Invalid
      for y in 0 .. (rows - 1) do
        let tempArr = inArr.[y].ToCharArray()
        let realY   = rows - 1 - y // Vertical flip
        for x in 0 .. (cols - 1) do
          outArr.[x, realY] <- Terrain.charToTerrain tempArr.[x]
      outArr

    let findStartAndGoal (arr: terrain [,]) (cols: int) (rows: int) =

      let mutable selfCoord: coordinate = BadCoord
      let mutable goalCoord: coordinate = BadCoord

      for x in 0 .. (cols - 1) do
        for y in 0 .. (rows - 1) do
          let c = arr.[x, y]
          if   c = Self then selfCoord <- Coord(x, y)
          elif c = Goal then goalCoord <- Coord(x, y)

      if selfCoord = BadCoord then
        invalidArg "arr" "No start in given grid."
      elif goalCoord = BadCoord then
        invalidArg "arr" "No start in given grid."
      else
        (selfCoord, goalCoord)

    let fromPathingMapString = function
      | PathingMapString(str, delim) ->
          let splitArr      = str.Split ([|delim|], System.StringSplitOptions.None)
          let rows          = splitArr.Length
          let cols          = splitArr.[0].Length
          let outArr        = convertArray cols rows splitArr
          let (start, goal) = findStartAndGoal outArr cols rows
          PathingMapData(start, goal, cols, rows, outArr)
