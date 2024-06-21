/** @type {import('tailwindcss').Config} */
const colors = require('tailwindcss/colors');
module.exports = {
    content: [
        "./**/*.razor",
        "./**/*.html",
        "./**/*.cshtml",
        "./**/*.razor.cs",
        "./**/*.js",
        "./node_modules/flowbite/**/*.js"
    ],
    theme: {
        colors: {
            'blue': '#1fb6ff',
            'purple': '#7e5bef',
            'pink': '#ff49db',
            'orange': '#ff7849',
            'green': '#13ce66',
            'yellow': '#ffc82c',
            'gray-dark': '#273444',
            'gray': '#8492a6',
            'gray-light': '#d3dce6',
        },
        fontFamily: {
            sans: ['Graphik', 'sans-serif'],
            serif: ['Merriweather', 'serif'],
        },
        extend: {
            spacing: {
                '80': '20rem'
            },
            colors: {
                stone: colors.warmGray,
                sky: colors.lightBlue,
                neutral: colors.trueGray,
                gray: colors.coolGray,
                slate: colors.blueGray,

            },
            animationDelay: {
                275: '275ms',
                5000: '5s',
            },
            animationDuration: {
                2000: '2s',
                'long': '10s',
                'very-long': '20s',
            },
            spacing: {
                '8xl': '96rem',
                '9xl': '128rem',
            },
            borderRadius: {
                '4xl': '2rem',
            }
        },
        screens: {
            'sm': '640px',
            'md': '768px',
            'lg': '1024px',
            'xl': '1280px',
        },
    },
    plugins: [
        
        require('flowbite/plugin'),
        require('tailwindcss-animated')
    ],
}
