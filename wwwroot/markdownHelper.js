window.markdownHelper = {
    render: function (mdStr, divId) {
        const tm = texmath.use(katex);
        const md = markdownit().use(tm, { engine: katex,
                                        delimiters:'dollars',
                                        macros:{"\\RR": "\\mathbb{R}"}
                                        });
        divId.innerHTML = 
            md.render(mdStr);
    }
};