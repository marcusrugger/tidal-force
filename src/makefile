
SOURCE = 	tides.cs				\
			tidalvectors.cs			\
			constants.cs			\
			cartesian.cs			\
			polar.cs				\
			gravitationalforce.cs

NUNIT_PATH			=	/mnt/data/bin/nunit-3.2.1
NUNIT_FRAMEWORK		=	$(NUNIT_PATH)/bin/net-4.0/nunit.framework.dll


all:		tides.exe	\
			tests.dll	\
			runtests


clean:
	-rm tides.exe
	-rm tests.dll


runtests:	tests.dll
	mono /mnt/data/bin/nunit-3.2.1/bin/nunit3-console.exe tests.dll -noresult


tides.exe:	$(SOURCE)
	mcs -out:$@ -target:exe -pkg:dotnet $^


tests.dll:	test.polar.cs			\
			$(SOURCE)
	mcs -out:$@ -target:library -pkg:dotnet -r:$(NUNIT_FRAMEWORK) $^
