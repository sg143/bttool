namespace QuickTimeWpfLib
{
	public enum StatusCode
	{
		None=0,
		Connecting=2,
		Negotiating=5,
		RequestedData = 11,
		Buffering = 12,
		UrlChanged = 4096,
		FullScreenBegin = 4097,
		FullScreenEnd = 4098,
		MovieLoadFinalize = 4099,
		MovieLoadStateError = -1,
		MovieLoadStateLoading = 1000,
		MovieLoadStateLoaded = 2000,
		MovieLoadStatePlayable = 10000,
		MovieLoadStatePlaythroughOK = 20000,
		MovieLoadStateComplete = 100000
	}
}