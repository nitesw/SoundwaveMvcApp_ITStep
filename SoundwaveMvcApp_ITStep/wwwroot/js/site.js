// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
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
        });
    });
});