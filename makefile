
BIN_DIR		= bin
TIDES		= src
WINFORMS	= winforms
GTKSHARP	= gtk


all:	$(BIN_DIR)
	$(MAKE) -C $(TIDES)
	$(MAKE) -C $(WINFORMS)
	$(MAKE) -C $(GTKSHARP)


clean:
	$(MAKE) -C $(TIDES) clean
	$(MAKE) -C $(WINFORMS) clean
	$(MAKE) -C $(GTKSHARP) clean


makewinforms:
	$(MAKE) -C $(TIDES)
	$(MAKE) -C $(WINFORMS)


runwinforms:
	$(MAKE) -C $(WINFORMS) run


makegtk:
	$(MAKE) -C $(TIDES)
	$(MAKE) -C $(GTKSHARP)


rungtk:
	$(MAKE) -C $(GTKSHARP) run


$(BIN_DIR):
	mkdir $(BIN_DIR)
