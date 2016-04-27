
SOURCE = 	tides.cs					\
			tidewindow.winforms.cs		\
			controller.cs				\
			algorithms.cs				\
			animator.base.cs			\
			animator.moon.cs			\
			animator.sun.cs				\
			animator.sunmoon.cs			\
			presentation.base.cs		\
			presentation.gdiplus.cs		\
			tidalvectors.cs				\
			forcepoints.cs				\
			forcevectors.cs				\
			constants.cs				\
			cartesian.cs				\
			polar.cs					\
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


run:		tides.exe
	mono tides.exe


tides.exe:	$(SOURCE)
	mcs -out:$@ -target:exe -pkg:dotnet $^


tests.dll:	test.polar.cs				\
			test.cartesian.cs			\
			test.gravitationalforce.cs	\
			test.forcepoints.cs			\
			test.forcevectors.cs		\
			test.algorithms.cs			\
			$(SOURCE)
	mcs -out:$@ -target:library -pkg:dotnet -r:$(NUNIT_FRAMEWORK) $^
