/*
 ============================================================================
 Name		: DateDiff.pan
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : This file contains panic codes.
 ============================================================================
 */

#ifndef __DATEDIFF_PAN__
#define __DATEDIFF_PAN__

/** DateDiff application panic codes */
enum TDateDiffPanics
	{
	EDateDiffUi = 1
	// add further panics here
	};

inline void Panic(TDateDiffPanics aReason)
	{
	_LIT(applicationName, "DateDiff");
	User::Panic(applicationName, aReason);
	}

#endif // __DATEDIFF_PAN__
