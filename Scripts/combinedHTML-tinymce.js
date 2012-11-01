
function initTinyMCE(options) {
    tinyMCE.init({
        theme: "advanced",
        mode: "textareas",
        editor_selector: "tinymce",
        width: 1200,
        height: 400,
        plugins: "fullscreen,autoresize,searchreplace,template",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_buttons1: "search,replace,|,undo,redo,|,link,unlink,|,charmap,emoticon,|,codeblock,|,code,|,fullscreen,template",
        theme_advanced_buttons2: "",
        theme_advanced_buttons3: "",
        convert_urls: false,
        valid_elements: '*[*]',
        extended_valid_elements: "script[type|defer|src|language]",
        template_templates: options.templates,
        valid_children: "+a[div|h3]",
        content_css: options.Controller + "GetTemplateCss",
        document_base_url: options.Host,
        external_link_list_url: options.Controller + "GetLinksList",
        external_image_list_url: "",
        external_scripts_list: options.scripts,
        force_br_newlines: false,
        force_p_newlines: false,
        auto_focus: "HtmlDetail",
        setup: function (ed) {
            ed.onInit.add(function (ed) {
                //open source_editor  for resize
                tinyMCE.closeOnLoad = true;
                //                ed.windowManager.open({
                //                    url: ed.theme.url + '/source_editor.htm',
                //                    width: 600 + parseInt(ed.getLang('advanced.link_delta_width', 0)),
                //                    height: 200 + parseInt(ed.getLang('advanced.link_delta_height', 0)),
                //                    inline: true
                //                }, {
                //                    theme_url: ed.theme.url
                //                });
                //                
            });

        },
        extended_valid_elements: "iframe[name|src|framespacing|border|frameborder|scrolling|title|height|width],a[href|target|name|rel|class],map[name],area[shape|coords|href],object[declare|classid|codebase|data|type|codetype|archive|standby|height|width|usemap|name|tabindex|align|border|hspace|vspace|style],param[name|value],embed[src|type|width|height|flashvars|wmode],img[id|dir|lang|longdesc|usemap|style|class|src|onmouseover|onmouseout|border=0|alt|title|hspace|vspace|width|height|align|play|swliveconnect|loop|quality|scale|align|salign|wmode|bgcolor|base|flashvars] "
    });
    tinyMCE.Host = options.Host;
    tinyMCE.ContentRootFolder = options.ContentRootFolder.replace("img/", "");
    tinyMCE.Controller = options.Controller;
}

function ajaxLoad(apppath) {
    var host = apppath;
    var controller = "CombinedHTML/";
    $.ajax({
        type: 'GET',
        url: host + controller + 'GetTinyMceInitSettings',
        dataType: "json",
        success: function (data) {
            var options = {};
            options.templates = data.templates
            options.scripts = data.scripts;
            options.cssAction = host + controller + 'GetTemplateCss';
            options.Host = host;
            options.ContentRootFolder = data.rootImagesFolderPath;
            options.Controller = controller;
            initTinyMCE(options);
        },
        error: function (error) {
            alert(error);
        }
    });
} 