﻿namespace SoundwaveMvcApp_ITStep.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Song>? Songs { get; set; }
    }
}
