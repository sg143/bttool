using QTOLibrary;

namespace QuickTimeWpfLib
{
	public sealed class QuickTimeMovie
	{
		private QTMovie _movie;
		internal QuickTimeMovie(QTMovie movie)
		{
			_movie = movie;
		}

		public void Play()
		{
			_movie.Play(1);
		}
	}
}