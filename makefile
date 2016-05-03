
BIN_DIR		= bin
TIDES		= src
WINFORMS	= winforms
GTKSHARP	= gtk
FLATLAND	= lib/flatland


all:	$(BIN_DIR)	\
		flatland
	$(MAKE) -C $(TIDES)
	$(MAKE) -C $(WINFORMS)
	$(MAKE) -C $(GTKSHARP)


clean:
	$(MAKE) -C $(FLATLAND) clean
	$(MAKE) -C $(TIDES) clean
	$(MAKE) -C $(WINFORMS) clean
	$(MAKE) -C $(GTKSHARP) clean
	-rm -r -f $(BIN_DIR)


flatland:
	$(MAKE) -C $(FLATLAND)
	cp $(FLATLAND)/bin/flatland.*.dll $(BIN_DIR)


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
