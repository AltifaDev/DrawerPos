/** @type {import('tailwindcss').Config} */
const colors = require('tailwindcss/colors');
const { addDynamicIconSelectors } = require('@iconify/tailwind')

module.exports = {
    content: [
        './pages/*.razor}',
        "./**/*.razor",
        "./**/*.html",
        "./**/*.cshtml",
        "./**/*.razor.cs",
        "./**/*.js",
        "./node_modules/flowbite/**/*.js",
       
    ],
    safelist: [
        'w-64',
        'w-1/2',
        'rounded-l-lg',
        'rounded-r-lg',
        'bg-gray-200',
        'grid-cols-4',
        'grid-cols-7',
        'h-6',
        'leading-6',
        'h-9',
        'leading-9',
        'shadow-lg',
        'bg-opacity-50',
        'dark:bg-opacity-80'
    ],
    darkMode: "class",
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
            primary: { "50": "#eff6ff", "100": "#dbeafe", "200": "#bfdbfe", "300": "#93c5fd", "400": "#60a5fa", "500": "#3b82f6", "600": "#2563eb", "700": "#1d4ed8", "800": "#1e40af", "900": "#1e3a8a" }
        },
        fontFamily: {
            'sans': ['Inter', 'ui-sans-serif', 'system-ui', '-apple-system', 'system-ui', 'Segoe UI', 'Roboto', 'Helvetica Neue', 'Arial', 'Noto Sans', 'sans-serif', 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol', 'Noto Color Emoji'],
            'body': ['Inter', 'ui-sans-serif', 'system-ui', '-apple-system', 'system-ui', 'Segoe UI', 'Roboto', 'Helvetica Neue', 'Arial', 'Noto Sans', 'sans-serif', 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol', 'Noto Color Emoji'],
            'mono': ['ui-monospace', 'SFMono-Regular', 'Menlo', 'Monaco', 'Consolas', 'Liberation Mono', 'Courier New', 'monospace']
        },
        transitionProperty: {
            'width': 'width'
        },
        textDecoration: ['active'],
        minWidth: {
            'kanban': '28rem'
        },
        extend: {
            keyframes: {
                'text-shimmer': {
                    from: { backgroundPosition: '0 0' },
                    to: { backgroundPosition: '-200% 0' },
                },
            },
            animation: {
                'text-shimmer': 'text-shimmer 2.5s ease-out infinite alternate',
            },
            height: {
                '90': '24rem',
                '128': '32rem',
            },
            backdropBlur: {
                'sm': 'blur(4px)',
                'md': 'blur(8px)',
                'lg': 'blur(16px)',
            },
            backgroundOpacity: {
                '10': '0.1',
                '20': '0.2',
                '50': '0.5',
                '75': '0.75',
            },
            animationDuration: {
                2000: '2s',
                'long': '10s',
                'very-long': '20s',
            },
            spacing: {
                '8xl': '96rem',
                '9xl': '128rem',
                '90': '24rem',
                '80': '20rem',
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
 
        require('@tailwindcss/aspect-ratio'), function ({ addUtilities }) {
            const newUtilities = {
                '.hide-scrollbar': {
                    'scrollbar-width': 'none',
                    '-ms-overflow-style': 'none',
                },
                '.hide-scrollbar::-webkit-scrollbar': {
                    display: 'none',
                },
            };

            addUtilities(newUtilities, ['responsive', 'hover']);
        },
        require('flowbite/plugin'),
        require('tailwindcss-animated'),
        require('tailwindcss-filters'),
        addDynamicIconSelectors(),
    ],
}
