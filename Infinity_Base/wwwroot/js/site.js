// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const tooltips = document.querySelectorAll('.tt')
tooltips.forEach(t => {
    new bootstrap.Tooltip(t)
})


$('#treeview').jstree({
    "plugins": ["search", "wholerow"],
    'core': {
        'data': {
            'url': function (node) {
                return node.id === '#' ? "/Treeview/GetRoot" : "/Treeview/GetChildren/" + node.id;

            },
            'data': function (node) {
                return { 'id': node.id };
            }
        }
    }
});

$('#treeview').on('changed.jstree', function (e, data) {
    console.log("=> selected node: " + data.node.id);
});
