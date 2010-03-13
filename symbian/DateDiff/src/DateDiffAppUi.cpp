/*
 ============================================================================
 Name		: DateDiffAppUi.cpp
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : CDateDiffAppUi implementation
 ============================================================================
 */

/*
Copyright 2009 http://code.google.com/p/toolkits/. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:
  * Redistributions of source code must retain the above copyright
    notice, this list of conditions and the following disclaimer.
  * Redistributions in binary form must reproduce the above
    copyright notice, this list of conditions and the following
    disclaimer in the documentation and/or other materials provided
    with the distribution.
  * Neither the name of http://code.google.com/p/toolkits/ nor the names of its
    contributors may be used to endorse or promote products derived
    from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

// INCLUDE FILES
#include <avkon.hrh>
#include <aknmessagequerydialog.h>
#include <aknnotewrappers.h>
#include <stringloader.h>
#include <f32file.h>
#include <s32file.h>
#include <hlplch.h>
#include <aknnotewrappers.h>
#include <DateDiff_0xE8735039.rsg>

#include "DateDiff_0xE8735039.hlp.hrh"
#include "DateDiff.hrh"
#include "DateDiff.pan"
#include "DateDiffApplication.h"
#include "DateDiffAppUi.h"
#include "DateDiffAppView.h"

_LIT( KFileName, "C:\\private\\E8735039\\DateDiff.txt" );
_LIT( KText, "Date Diff");


TInt DiffDaysNumbers( const TTime & aTime1, const TTime & aTime2 )
	{
	/*计算两个TTime对象相隔多少天需要把TTime对象的时分秒等小单位改成0或相同的值,
	 *否则在有的情况下得到的结果不准确*/
	TDateTime date = aTime1.DateTime();
	date.SetHour( 0 );
	date.SetMinute( 0 );
	date.SetSecond( 0 );
	date.SetMicroSecond( 0 );
	TTime time1 = date;
	date = aTime2.DateTime();
	date.SetHour( 0 );
	date.SetMinute( 0 );
	date.SetSecond( 0 );
	date.SetMicroSecond( 0 );
	TTime time2 = date;
	return time2.DaysFrom( time1 ).Int();
}

// ============================ MEMBER FUNCTIONS ===============================


// -----------------------------------------------------------------------------
// CDateDiffAppUi::ConstructL()
// Symbian 2nd phase constructor can leave.
// -----------------------------------------------------------------------------
//
void CDateDiffAppUi::ConstructL()
	{
	// Initialise app UI with standard value.
	BaseConstructL(CAknAppUi::EAknEnableSkin);

	// Create view object
	iAppView = CDateDiffAppView::NewL(ClientRect());

	// Create a file to write the text to
	TInt err = CCoeEnv::Static()->FsSession().MkDirAll(KFileName);
	if ((KErrNone != err) && (KErrAlreadyExists != err))
		{
		return;
		}

	RFile file;
	err = file.Replace(CCoeEnv::Static()->FsSession(), KFileName, EFileWrite);
	CleanupClosePushL(file);
	if (KErrNone != err)
		{
		CleanupStack::PopAndDestroy(1); // file
		return;
		}

	RFileWriteStream outputFileStream(file);
	CleanupClosePushL(outputFileStream);
	outputFileStream << KText;

	CleanupStack::PopAndDestroy(2); // outputFileStream, file

	}
// -----------------------------------------------------------------------------
// CDateDiffAppUi::CDateDiffAppUi()
// C++ default constructor can NOT contain any code, that might leave.
// -----------------------------------------------------------------------------
//
CDateDiffAppUi::CDateDiffAppUi()
	{
	// No implementation required
	}

// -----------------------------------------------------------------------------
// CDateDiffAppUi::~CDateDiffAppUi()
// Destructor.
// -----------------------------------------------------------------------------
//
CDateDiffAppUi::~CDateDiffAppUi()
	{
	if (iAppView)
		{
		delete iAppView;
		iAppView = NULL;
		}

	}

// -----------------------------------------------------------------------------
// CDateDiffAppUi::HandleCommandL()
// Takes care of command handling.
// -----------------------------------------------------------------------------
//
void CDateDiffAppUi::HandleCommandL(TInt aCommand)
	{
	switch (aCommand)
		{
		case EEikCmdExit:
		case EAknSoftkeyExit:
			Exit();
			break;

		case ECommandDiffDate:
			{
			
			TTime begTime;
			begTime.HomeTime();
			TTime endTime;
			endTime.HomeTime();
			CAknMultiLineDataQueryDialog* dlg =
			        CAknMultiLineDataQueryDialog::NewL(begTime, endTime);
            TBuf <32> cancelInfo;
            CEikonEnv::Static()->ReadResource(cancelInfo, R_CANCEL_TEXT);
            
			if (dlg->ExecuteLD(R_AKNEXQUERY_DATE_QUERY)) {
				TBuf<256> info;
				TInt num = DiffDaysNumbers(begTime, endTime);
				info.AppendNum(num);
				
				CAknInformationNote * inforNote;
				inforNote = new (ELeave) CAknInformationNote;
				inforNote->ExecuteLD(info);
			}
			else {
                CAknInformationNote * inforNote;
                inforNote = new (ELeave) CAknInformationNote;
                inforNote->ExecuteLD(cancelInfo);
			}
			
			}
			break;
		
		case ECommandDateAdd:
			{
            TTime begTime;
            begTime.HomeTime();
            TTimeIntervalSeconds  endTime;
            CAknTimeQueryDialog* dlg = 
				CAknTimeQueryDialog::NewL( begTime);

            TBuf <32> cancelInfo;
            CEikonEnv::Static()->ReadResource(cancelInfo, R_CANCEL_TEXT);
            
            if (dlg->ExecuteLD(R_DATE_QUERY)) {
                TBuf<256> info;
                _LIT(formatter,"%F%Y/%M/%D");
                TInt sec(0);
                CAknNumberQueryDialog* num_dlg = 
					new (ELeave) CAknNumberQueryDialog (sec);
                num_dlg->PrepareLC(R_NUMBER_QUERY);
                if(num_dlg->RunLD()) {
					TTimeIntervalSeconds tint = TTimeIntervalSeconds(sec*60*60*24);
					begTime += tint;
                    begTime.FormatL(info, formatter);
                    CAknInformationNote * inforNote;
                    inforNote = new (ELeave) CAknInformationNote;
                    inforNote->ExecuteLD(info);
                }
                else {
					CAknInformationNote * inforNote;
					inforNote = new (ELeave) CAknInformationNote;
					inforNote->ExecuteLD(cancelInfo);                
                }
                
            }
            else {
                CAknInformationNote * inforNote;
                inforNote = new (ELeave) CAknInformationNote;
                inforNote->ExecuteLD(cancelInfo);
            }			
			//*/
			}
			break;
		case EAbout:
			{
			CAknMessageQueryDialog* dlg = new (ELeave) CAknMessageQueryDialog();
			dlg->PrepareLC(R_ABOUT_QUERY_DIALOG);
			HBufC* title = iEikonEnv->AllocReadResourceLC(R_ABOUT_DIALOG_TITLE);
			dlg->QueryHeading()->SetTextL(*title);
			CleanupStack::PopAndDestroy(); //title
			HBufC* msg = iEikonEnv->AllocReadResourceLC(R_ABOUT_DIALOG_TEXT);
			dlg->SetMessageTextL(*msg);
			CleanupStack::PopAndDestroy(); //msg
			dlg->RunLD();
			}
			break;
		default:
			Panic(EDateDiffUi);
			break;
		}
	}
// -----------------------------------------------------------------------------
//  Called by the framework when the application status pane
//  size is changed.  Passes the new client rectangle to the
//  AppView
// -----------------------------------------------------------------------------
//
void CDateDiffAppUi::HandleStatusPaneSizeChange()
	{
	iAppView->SetRect(ClientRect());
	}

CArrayFix<TCoeHelpContext>* CDateDiffAppUi::HelpContextL() const
	{
#warning "Please see comment about help and UID3..."
	// Note: Help will not work if the application uid3 is not in the
	// protected range.  The default uid3 range for projects created
	// from this template (0xE0000000 - 0xEFFFFFFF) are not in the protected range so that they
	// can be self signed and installed on the device during testing.
	// Once you get your official uid3 from Symbian Ltd. and find/replace
	// all occurrences of uid3 in your project, the context help will
	// work. Alternatively, a patch now exists for the versions of 
	// HTML help compiler in SDKs and can be found here along with an FAQ:
	// http://www3.symbian.com/faq.nsf/AllByDate/E9DF3257FD565A658025733900805EA2?OpenDocument
	CArrayFixFlat<TCoeHelpContext>* array = new (ELeave) CArrayFixFlat<
			TCoeHelpContext> (1);
	CleanupStack::PushL(array);
	array->AppendL(TCoeHelpContext(KUidDateDiffApp, KGeneral_Information));
	CleanupStack::Pop(array);
	return array;
	}

// End of File
