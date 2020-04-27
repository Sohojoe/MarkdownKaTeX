window.markdownHelper = {
    // delimiters 'dollars', 'brackets',, 'gitlab', 'julia' 'kramdown', 'pandoc'
    render: function (mdStr, divId, delimiters = 'dollars') {
        const tm = texmath.use(katex);
        const md = markdownit().use(tm, { engine: katex,
                                        delimiters: delimiters,
                                        // delimiters:'kramdown',
                                        macros:{"\\RR": "\\mathbb{R}"}
                                        });
        divId.innerHTML = 
            md.render(mdStr);
    }
};