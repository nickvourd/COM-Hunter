BOFNAME := COM-Hunter
LIBINCLUDE := 
CC_x64 := x86_64-w64-mingw32-gcc
CC_x86 := i686-w64-mingw32-gcc
CC=x86_64-w64-mingw32-clang

all:
	$(CC_x64) -o $(BOFNAME).x64.o -Os -c COM-Hunter.c
	$(CC_x86) -o $(BOFNAME).x86.o -Os -c COM-Hunter.c 

clean:
	rm $(BOFNAME).*.o
