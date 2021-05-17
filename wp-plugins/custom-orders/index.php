<?php
/**

* Plugin Name: Custom orders

* Plugin URI: http://www.brainbeast.best

* Description: Custom orders

* Version: 1.0

* Author: Rutherford

* Author URI: http://www.brainbeast.best

*/

function wp_orders_init() {
    $labels = array(
        'name'                  => _x( 'Orders', 'Post type general name', 'textdomain' ),
        'singular_name'         => _x( 'Order', 'Post type singular name', 'textdomain' ),
        'menu_name'             => _x( 'Orders', 'Admin Menu text', 'textdomain' ),
        'name_admin_bar'        => _x( 'Order', 'Add New on Toolbar', 'textdomain' ),
        'add_new'               => __( 'Add New', 'textdomain' ),
        'add_new_item'          => __( 'Add New Order', 'textdomain' ),
        'new_item'              => __( 'New Order', 'textdomain' ),
        'edit_item'             => __( 'Edit Order', 'textdomain' ),
        'view_item'             => __( 'View Order', 'textdomain' ),
        'all_items'             => __( 'All Orders', 'textdomain' ),
        'search_items'          => __( 'Search Orders', 'textdomain' ),
        'parent_item_colon'     => __( 'Parent Order:', 'textdomain' ),
        'not_found'             => __( 'No Orders found.', 'textdomain' ),
        'not_found_in_trash'    => __( 'No Orders found in Trash.', 'textdomain' ),
        'featured_image'        => _x( 'Order Cover Image', 'Overrides the “Featured Image” phrase for this post type. Added in 4.3', 'textdomain' ),
        'set_featured_image'    => _x( 'Set cover image', 'Overrides the “Set featured image” phrase for this post type. Added in 4.3', 'textdomain' ),
        'remove_featured_image' => _x( 'Remove cover image', 'Overrides the “Remove featured image” phrase for this post type. Added in 4.3', 'textdomain' ),
        'use_featured_image'    => _x( 'Use as cover image', 'Overrides the “Use as featured image” phrase for this post type. Added in 4.3', 'textdomain' ),
        'archives'              => _x( 'Order archives', 'The post type archive label used in nav menus. Default “Post Archives”. Added in 4.4', 'textdomain' ),
        'insert_into_item'      => _x( 'Insert into Order', 'Overrides the “Insert into post”/”Insert into page” phrase (used when inserting media into a post). Added in 4.4', 'textdomain' ),
        'uploaded_to_this_item' => _x( 'Uploaded to this Order', 'Overrides the “Uploaded to this post”/”Uploaded to this page” phrase (used when viewing media attached to a post). Added in 4.4', 'textdomain' ),
        'filter_items_list'     => _x( 'Filter Orders list', 'Screen reader text for the filter links heading on the post type listing screen. Default “Filter posts list”/”Filter pages list”. Added in 4.4', 'textdomain' ),
        'items_list_navigation' => _x( 'Orders list navigation', 'Screen reader text for the pagination heading on the post type listing screen. Default “Posts list navigation”/”Pages list navigation”. Added in 4.4', 'textdomain' ),
        'items_list'            => _x( 'Orders list', 'Screen reader text for the items list heading on the post type listing screen. Default “Posts list”/”Pages list”. Added in 4.4', 'textdomain' ),
    );
 
    $args = array(
        'labels'             => $labels,
        'public'             => true,
        'publicly_queryable' => true,
        'show_ui'            => true,
        'show_in_menu'       => true,
        'capability_type'    => 'post',
        'has_archive'        => true,
        'hierarchical'       => false,
        'menu_position'      => null,
        'supports'           => array( 'title', ),
    );
 
    register_post_type( 'order', $args );
    
}
 
add_action( 'init', 'wp_orders_init' );

class Orders_Meta_Box {
 
    public $_post_type;

    public $_meta_box;

    function __construct($pt, $mb) {
        $this->_post_type = $pt;
        $this->_meta_box = $mb;
    }
    
    /**
     * Set up and add the meta box.
     */
    public function add() {
        add_meta_box(
            $this->_meta_box,          // Unique ID
            $this->_meta_box, // Box title
            [ $this, 'html' ],   // Content callback, must be of type callable
            $this->_post_type                  // Post type
        );
    }
 
 
    /**
     * Save the meta box selections.
     *
     * @param int $post_id  The post ID.
     */
    public function save( int $post_id ) {
        if ( array_key_exists( $this->_meta_box, $_POST ) ) {
            update_post_meta(
                $post_id,
                $this->_meta_box,
                $_POST[$this->_meta_box]
            );
        }
        
    }
 
 
    /**
     * Display the meta box HTML to the user.
     *
     * @param \WP_Post $post   Post object.
     */
    public function html( $post ) {
        
        $value = get_post_meta( $post->ID, $this->_meta_box, true );

        if($this->_meta_box=="orders") :
        ?>

            <label for="<?php echo $this->_meta_box; ?>">Products: </label>
            <input name="<?php echo $this->_meta_box; ?>" id="<?php echo $this->_meta_box; ?>" class="postbox" type="text" value="<?php echo $value; ?>"/>
            
        <?php
        else:
        ?>

            <label for="<?php echo $this->_meta_box; ?>">Value: </label>
            <input name="<?php echo $this->_meta_box; ?>" id="<?php echo $this->_meta_box; ?>" class="postbox" type="text" value="<?php echo $value; ?>"/>        

        <?php
        endif;
    }
}

$meta_boxes = [
    "orders",
    "ids",
    "name",
    "second_name",
    "phone",
    "address",
];

foreach ($meta_boxes as $mb) {

    $box = new Orders_Meta_Box("order", $mb);
    add_action( 'add_meta_boxes', [ $box, 'add' ] );
    add_action( 'save_post', [ $box, 'save' ] );
}