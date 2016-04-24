
SOURCE = 	tides.cs				\
			tidalvectors.cs			\
			constants.cs			\
			cartesian.cs			\
			polar.cs				\
			gravitationalforce.cs

NUNIT_VERSION		=	nunit-3.2.1
NUNIT_PATH			=	/mnt/data/bin/$(NUNIT_VERSION)
NUNIT_FRAMEWORK		=	$(NUNIT_PATH)/bin/net-4.0/nunit.framework.dll
NUNIT_CONSOLE		=	$(NUNIT_PATH)/bin/nunit3-console.exe


all:		tides.exe	\
			tests.dll	\
			runtests


clean:
	-rm tides.exe
	-rm tests.dll
	-rm tests.mdb


runtests:	tests.dll
	mono $(NUNIT_CONSOLE) tests.dll -noresult


tides.exe:	$(SOURCE)
	mcs -out:$@ -target:exe -pkg:dotnet $^


tests.dll:	test.polar.cs				\
			test.cartesian.cs			\
			test.gravitationalforce.cs	\
			$(SOURCE)
	mcs -out:$@ -target:library -pkg:dotnet -r:$(NUNIT_FRAMEWORK) $^
