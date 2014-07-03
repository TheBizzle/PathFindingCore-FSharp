ROOT=./src/main/
COORD=$(ROOT)coordinate/
PMAP=$(ROOT)pathingmap/

SRCS_ROOT=$(ROOT)stepdata.fs $(ROOT)pathingstatus.fs
SRCS_COORD=$(COORD)coordinate.fs
SRCS_PMAP=$(PMAP)terrain.fs $(PMAP)direction.fs $(PMAP)pathingmapstringops.fs $(PMAP)pathingmap.fs

SRCS=$(SRCS_COORD) $(SRCS_PMAP) $(SRCS_ROOT)

OUT_DLL=pathfindingcore.dll

$(OUT_DLL): $(SRCS)
		fsharpc $(SRCS) -a -o $(OUT_DLL)

