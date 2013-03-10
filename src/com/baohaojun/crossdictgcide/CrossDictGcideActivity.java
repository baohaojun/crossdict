package com.baohaojun.crossdictgcide;

import android.app.Activity;
import android.os.Bundle;
import android.view.WindowManager;
import android.view.Window;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Button;
import android.widget.EditText;
import android.view.View.OnClickListener;
import android.view.View;
import java.util.ArrayList;
import android.widget.AdapterView;
import android.util.Log;
import android.widget.TextView;
import android.view.KeyEvent;
import android.content.DialogInterface;
import android.app.AlertDialog;
import java.io.OutputStreamWriter;
import java.io.FileOutputStream;
import android.content.Context;
import java.io.InputStreamReader;
import android.os.Handler;
import android.view.MenuItem;
import android.widget.Toast;
import android.view.Menu;
import java.util.HashMap;
import android.os.Environment;
import android.content.res.AssetManager;
import java.io.File;
import java.io.InputStream;
import java.io.OutputStream;
import android.net.Uri;
import android.content.Intent;
import android.os.AsyncTask;
import android.app.Dialog;
import android.app.ProgressDialog;

public class CrossDictGcideActivity extends Activity {
    /** Called when the activity is first created. */
    private EditText mEdit; 

    private Button mLookUpButton;
    private Button mDefinedButton;
    private Button mMatchingButton;
    private Button mListButton;


    private File mWorkingDir;

    private static final String[] dictFileBaseNames = {"ahd", "frequency", "usage", "words"};
    private static final String[] dictFileExtNames = {".dz", ".idx", ".ii"};
    private static ArrayList<String> mDictFiles = new ArrayList<String>();

    private static final int DIALOG_COPY_FILES = 0;

    static {
	for (String base : dictFileBaseNames) {
	    for (String ext : dictFileExtNames) {
		mDictFiles.add(base + ext);
	    }
	}
	
	String[] moreFiles = {"android.selection.js", "jquery.js", "rangy-core.js", "rangy-serializer.js", "dict.css"};
	for (String file : moreFiles) {
	    mDictFiles.add(file);
	}
    }

    private String checkDictFiles() {
	if (!mWorkingDir.exists()) {
	    return mWorkingDir.toString();
	}

	for (String file : mDictFiles) {
	    if (! new File(mWorkingDir, file).exists()) {
		return file;
	    }
	}
	return null;
    }


    Dialog mCopyFileDialog;
    private class LookupTask extends AsyncTask<String, String, String> {
	
	@Override
        protected void onPreExecute() {
	    CrossDictGcideActivity.this.runOnUiThread(new Runnable() {
		    public void run() {
			CrossDictGcideActivity.this.showDialog(DIALOG_COPY_FILES);
		    }
		});	    
	}
        /**
         * Perform the background query using {@link ExtendedWikiHelper}, which
         * may return an error message as the result.
         */
        @Override
        protected String doInBackground(String... args) {
	    try {
		mWorkingDir.mkdirs();
		AssetManager am = getAssets();

		byte[] buf = new byte[4096];
		for (String fileName : mDictFiles) {
		    InputStream input = am.open(fileName);
		    OutputStream out = new FileOutputStream(new File(mWorkingDir, fileName));
		    while (true) {
			int n = input.read(buf);
			if (n <= 0) {
			    break;
			}
			out.write(buf, 0, n);
		    }
		    out.close();
		    input.close();
		}
	    } catch (Exception e) {
		Log.e("bhj", String.format("Error creating files\n"), e);
	    } finally {
		if (checkDictFiles() != null) {
		    new AlertDialog.Builder(CrossDictGcideActivity.this)
			.setTitle("Error!")
			.setMessage(String.format("Failed to create dictionary file '%s', will now exit.", checkDictFiles()))
			.setPositiveButton("OK", new DialogInterface.OnClickListener() {
				public void onClick(DialogInterface dialog, int whichButton) {
				    CrossDictGcideActivity.this.finish();
				}
			    })
			.create().show();
		    return "";
		}
	    }

	    if (mCopyFileDialog != null) {
		mCopyFileDialog.cancel();
		CrossDictGcideActivity.this.runOnUiThread(new Runnable() {
			public void run() {
			    continueLoading();
			}
		    });
	    }
            return "";
        }
    }

    @Override
    protected Dialog onCreateDialog(int id) {
        switch (id) {
            case DIALOG_COPY_FILES: {
                ProgressDialog dialog = new ProgressDialog(this);
                dialog.setMessage("Please wait while creating dictionary files...");
                dialog.setIndeterminate(true);
                dialog.setCancelable(false);
		mCopyFileDialog = dialog;
                return dialog;
            }
        }
        return null;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

	requestWindowFeature(Window.FEATURE_NO_TITLE);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, 
			     WindowManager.LayoutParams.FLAG_FULLSCREEN);
	
	mWorkingDir = Environment.getExternalStoragePublicDirectory("crossdict/ahd");

        setContentView(R.layout.main);
	if (checkDictFiles() != null) {
	    new LookupTask().execute("");
	} else {
	    continueLoading();
	}
    }

    private void continueLoading() {
	new AlertDialog.Builder(CrossDictGcideActivity.this)
	    .setTitle("Done!")
	    .setMessage("The GCIDE dictionary data for CrossDict has been copied, you can now uninstall this APK.")
	    .setPositiveButton("OK", new DialogInterface.OnClickListener() {
		    public void onClick(DialogInterface dialog, int whichButton) {
			CrossDictGcideActivity.this.finish();
		    }
		})
	    .create()
	    .show();
    }
}
