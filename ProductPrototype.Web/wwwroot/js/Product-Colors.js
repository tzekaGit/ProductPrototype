function createProductColors(productId) {
    if (productAttributeValues == null) return;
    const $container = $('#spheresContainer');

    // Group options by type
    const groups = {};


    productAttributeValues
        //.filter(v => v.ProductId === productId)
        .forEach(function (attr) {
            const val = attr.name;
            if (!val) return;
            const parts = val.split('|');
            let type = "";
            let colorName = "";
            if (parts.length < 2) {
                type = "color";
                colorName = parts[0].trim().toLowerCase().replace(/\s+/g, '-');
            }
            else
            { 
                type = parts[0].trim();
                colorName = parts[1].trim().toLowerCase().replace(/\s+/g, '-');
            }
            if (!groups[type]) groups[type] = [];
            groups[type].push({ name: colorName });
        });

    // Build frame dynamically
    for (const type in groups) {
        // Frame title
        const $title = $('<div></div>').addClass('frame-title').text(type + ' Finish Colors');
        $container.append($title);

        // Frame
        const $frame = $('<div></div>').addClass('frame');

        // Sphere set
        const $sphereSet = $('<div></div>').addClass('sphere-set');
        groups[type].forEach(function (c) {
            const $wrapper = $('<div></div>').addClass('sphere-wrapper');

            const $sphere = $('<div></div>').addClass('sphere').addClass(c.name);

            // Safe label
            const parts = c.fullText > 0 ? c.fullText?.split('|') : "";
            const $label = $('<div></div>').addClass('sphere-label').text(c.name.trim());

            $wrapper.append($sphere).append($label);
            $sphereSet.append($wrapper);
        });

        $frame.append($sphereSet);

        // Top border matches width of sphere-set


        // Right vertical
        const $vertical = $('<div></div>').addClass('vertical-right');
        $frame.append($vertical);

        $container.append($frame);

        // Adjust top-border width to match sphere-set
        const width = $sphereSet.outerWidth();
    }
};