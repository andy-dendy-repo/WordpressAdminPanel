<?php
add_action( 'wp_enqueue_scripts', 'my_theme_enqueue_styles' );

function my_theme_enqueue_styles() {
    $parenthandle = 'timesnews-style'; // This is 'twentyfifteen-style' for the Twenty Fifteen theme.
    $theme = wp_get_theme();
    wp_enqueue_style( $parenthandle, get_template_directory_uri() . '/style.css', 
        array(),  // if the parent theme code has a dependency, copy it to here
    );
    wp_enqueue_style( 'child-style', get_stylesheet_uri(),
        array( $parenthandle ),
        $theme->get('Version') // this only works if you have Version in the style header
    );
    wp_enqueue_script( 'custom-script', get_stylesheet_directory_uri() . '/script.js', array(), null, true );
}

function process_order() {
    $my_post = array(
    'post_status'   => 'publish',
    'post_author'   => 1,
    'post_type'     => "order",
    'post_title'    => date("Y-m-d H:i:s"),
    );

    $post_id = wp_insert_post( $my_post );

    $meta_boxes = [
        "orders",
        "ids",
        "name",
        "second_name",
        "phone",
        "address",
    ];

    $values = $_GET["process_order"];

    $value_arr = explode(";", $values);

    $dict = array(
        'name' => $value_arr[1],
        'second_name' => $value_arr[2],
        'phone' => $value_arr[3],
        'address' => $value_arr[4],
    );

    foreach ($meta_boxes as $mb) {

        update_post_meta(
                    $post_id,
                    $mb,
                    $dict[$mb],
                );
    }

    update_post_meta(
                    $post_id,
                    "ids",
                    $_COOKIE['products'],
                );
    

    setcookie('products', null, -1, '/'); 
}
