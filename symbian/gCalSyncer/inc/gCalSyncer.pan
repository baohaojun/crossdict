/*
 ============================================================================
 Name		: gCalSyncer.pan
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : This file contains panic codes.
 ============================================================================
 */

#ifndef __GCALSYNCER_PAN__
#define __GCALSYNCER_PAN__

/** gCalSyncer application panic codes */
enum TgCalSyncerPanics
	{
	EgCalSyncerUi = 1
	// add further panics here
	};

inline void Panic(TgCalSyncerPanics aReason)
	{
	_LIT(applicationName, "gCalSyncer");
	User::Panic(applicationName, aReason);
	}

#endif // __GCALSYNCER_PAN__
