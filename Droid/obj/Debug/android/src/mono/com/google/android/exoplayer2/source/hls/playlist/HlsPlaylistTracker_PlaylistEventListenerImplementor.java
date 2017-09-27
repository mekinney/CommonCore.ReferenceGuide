package mono.com.google.android.exoplayer2.source.hls.playlist;


public class HlsPlaylistTracker_PlaylistEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.source.hls.playlist.HlsPlaylistTracker.PlaylistEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPlaylistBlacklisted:(Lcom/google/android/exoplayer2/source/hls/playlist/HlsMasterPlaylist$HlsUrl;J)V:GetOnPlaylistBlacklisted_Lcom_google_android_exoplayer2_source_hls_playlist_HlsMasterPlaylist_HlsUrl_JHandler:Com.Google.Android.Exoplayer2.Source.Hls.Playlist.HlsPlaylistTracker/IPlaylistEventListenerInvoker, ExoPlayer.Hls\n" +
			"n_onPlaylistChanged:()V:GetOnPlaylistChangedHandler:Com.Google.Android.Exoplayer2.Source.Hls.Playlist.HlsPlaylistTracker/IPlaylistEventListenerInvoker, ExoPlayer.Hls\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Source.Hls.Playlist.HlsPlaylistTracker+IPlaylistEventListenerImplementor, ExoPlayer.Hls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HlsPlaylistTracker_PlaylistEventListenerImplementor.class, __md_methods);
	}


	public HlsPlaylistTracker_PlaylistEventListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HlsPlaylistTracker_PlaylistEventListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Source.Hls.Playlist.HlsPlaylistTracker+IPlaylistEventListenerImplementor, ExoPlayer.Hls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onPlaylistBlacklisted (com.google.android.exoplayer2.source.hls.playlist.HlsMasterPlaylist.HlsUrl p0, long p1)
	{
		n_onPlaylistBlacklisted (p0, p1);
	}

	private native void n_onPlaylistBlacklisted (com.google.android.exoplayer2.source.hls.playlist.HlsMasterPlaylist.HlsUrl p0, long p1);


	public void onPlaylistChanged ()
	{
		n_onPlaylistChanged ();
	}

	private native void n_onPlaylistChanged ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
