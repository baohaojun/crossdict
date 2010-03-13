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
package com.googlecode.toolkits;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.TextView;

public class DatDiffMain extends Activity {
    private TextView mDiffDisplay;
    private DatePicker begdate;
    private DatePicker enddate;
    private Button mCalcDiff;
    final private long DAY = 24L * 60L * 60L * 1000L;
    
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        
        mDiffDisplay = (TextView) findViewById(R.id.TextResult);
        mCalcDiff = (Button) findViewById(R.id.ButtonOK);
        
        mCalcDiff.setOnClickListener(new  View.OnClickListener() {
            public void onClick(View v) {
            	Date BegDay = new GregorianCalendar(
            			begdate.getYear(),
            			begdate.getMonth(),
            			begdate.getDayOfMonth()).getTime();
            	Date EndDay = new GregorianCalendar(
            			enddate.getYear(),
            			enddate.getMonth(),
            			enddate.getDayOfMonth()).getTime();
            	long numdays = EndDay.getTime() - BegDay.getTime();
            	numdays /= DAY;
            	SimpleDateFormat df = new SimpleDateFormat( "yyyy/MM/dd" );
            	mDiffDisplay.setText(" The number of days between " + df.format(BegDay)+
            			"and " + df.format(EndDay) +
            			"is : "+ numdays);
            }
        });
        
        final Calendar c = Calendar.getInstance();
        begdate = (DatePicker)findViewById(R.id.DatePickerBeginDate);
        enddate = (DatePicker)findViewById(R.id.DatePickerEndDate);
        begdate.init(c.get(Calendar.YEAR), c.get(Calendar.MONTH), c.get(Calendar.DAY_OF_MONTH)
        		,null);
        enddate.init(c.get(Calendar.YEAR), c.get(Calendar.MONTH), c.get(Calendar.DAY_OF_MONTH)
        		,null);
        
    }
}