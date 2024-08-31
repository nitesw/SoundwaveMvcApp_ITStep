function parseDate(dateString) {
    const [day, month, year, hours, minutes, seconds] = dateString.split(/[\s.:]+/);
    return new Date(year, month - 1, day, hours, minutes, seconds);
}

function timeAgo(currentDateStr, uploadDateStr) {
    const currentDate = parseDate(currentDateStr);
    const uploadDate = parseDate(uploadDateStr);
    const seconds = Math.floor((currentDate - uploadDate) / 1000);
    const interval = Math.floor(seconds / 31536000);

    if (interval > 1) return `${interval} years ago`;
    if (interval === 1) return `1 year ago`;
    const months = Math.floor(seconds / 2592000);
    if (months > 1) return `${months} months ago`;
    if (months === 1) return `1 month ago`;
    const days = Math.floor(seconds / 86400);
    if (days > 1) return `${days} days ago`;
    if (days === 1) return `1 day ago`;
    const hours = Math.floor(seconds / 3600);
    if (hours > 1) return `${hours} hours ago`;
    if (hours === 1) return `1 hour ago`;
    const minutes = Math.floor(seconds / 60);
    if (minutes > 1) return `${minutes} minutes ago`;
    if (minutes === 1) return `1 minute ago`;
    return `Just now`;
}

document.addEventListener('DOMContentLoaded', () => {
    const audioPlayer = new Plyr('#footer-audio-player', {
        controls: [
            'play',
            'progress',
            'current-time',
            'mute',
            'volume',
        ]
    });
    const loopButton = document.getElementById('loop-toggle');
    const loopIcon = document.getElementById('loop-icon');
    const audioSource = document.getElementById('footer-audio-source');
    const playTrackButtons = document.querySelectorAll('.play-track-button');
    let isLooping = false;

    function toggleLoopIcon() {
        if (isLooping) {
            loopIcon.classList.remove('bi-repeat');
            loopIcon.classList.add('bi-repeat-1');
        } else {
            loopIcon.classList.remove('bi-repeat-1');
            loopIcon.classList.add('bi-repeat');
        }
    }
    loopButton.addEventListener('click', function () {
        isLooping = !isLooping;
        toggleLoopIcon();
    });

    audioPlayer.on('ended', function () {
        if (isLooping) {
            audioPlayer.restart();
            audioPlayer.play();
        }
    });

    playTrackButtons.forEach(button => {
        button.addEventListener('click', function () {
            const trackSrc = this.parentElement.getAttribute('data-src');
            const playIcon = this.querySelector('img[data-state="play"]');
            const pauseIcon = this.querySelector('img[data-state="pause"]');

            if (audioPlayer.playing) {
                audioPlayer.pause();
                playIcon.style.display = 'block';
                pauseIcon.style.display = 'none';
            } else {
                audioSource.src = trackSrc;
                audioPlayer.source = {
                    type: 'audio',
                    sources: [
                        {
                            src: trackSrc,
                            type: 'audio/mp3'
                        }
                    ]
                };
                audioPlayer.play();
                playIcon.style.display = 'none';
                pauseIcon.style.display = 'block';
            }
        });
    });

    const dateElements = document.querySelectorAll('.upload-date');
    dateElements.forEach(el => {
        const currentDate = el.getAttribute('data-currentDate');
        const uploadDate = el.getAttribute('data-uploadDate');
        el.textContent = timeAgo(currentDate, uploadDate);
    });
});