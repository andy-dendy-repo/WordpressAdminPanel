<?php
/**
 * Template part for displaying posts
 *
 * @link https://developer.wordpress.org/themes/basics/template-hierarchy/
 *
 * @package timesnews
 */

?>
<?php 
$disable_category = get_theme_mod('disable-cateogry',0);
$disable_date = get_theme_mod('disable-date',0);
$disable_author = get_theme_mod('disable-author',0);
$disable_comments = get_theme_mod('disable-comments',0);
$tags_list = get_the_tag_list( '', esc_html_x( ', ', 'list item separator', 'timesnews' ) );
$excerpt_display = get_theme_mod('excerpt-display','excerpt-content');
$sticky_text = get_theme_mod ('sticky_text',esc_html__('Featured','timesnews'));

$articul = get_post_meta( get_the_ID(), 'articul', true );
$charasteristics = get_post_meta( get_the_ID(), 'charasteristics', true );
$description = get_post_meta( get_the_ID(), 'description', true );
$discount = get_post_meta( get_the_ID(), 'discount', true );
$price = get_post_meta( get_the_ID(), 'price', true );
?>
<article id="post-<?php the_ID(); ?>" class="page status-publish has-post-thumbnail hentry entry">
	<?php 
	$disable_featured_image_single = get_theme_mod('disable_featured_image_single',0);
	

	if ( is_sticky() ) { ?>

		<div class="sticky-post-tag">

			<span class="sticky-name"><?php echo esc_html( $sticky_text ); ?></span>

   	</div>

	<?php } ?>

	<div class="entry-content-holder">
		<header class="entry-header">

		<?php
				if($disable_category ==0) { ?>

				<div class="entry-meta">

					<?php timesnews_cat_lists (); ?>

				</div><!-- .entry-meta -->

		<?php }

		$excerpt_text = get_theme_mod('excerpt_text',esc_html__('Read More','timesnews'));

		if ( is_singular() ) :

				the_title( '<h1 class="entry-title">', '</h1>' );

			else :

				the_title( '<h2 class="entry-title"><a href="' . esc_url( get_permalink() ) . '" rel="bookmark">', '</a></h2>' );

		if( $disable_date ==0 || $disable_author ==0 ){ ?>

		<div class="entry-meta">
			<?php
			if($disable_date ==0){

				timesnews_posted_on();

			}
			?>
		</div><!-- .entry-meta -->

		<?php } 

		endif; ?>

	</header><!-- .entry-header -->

	<div class="entry-content">
		<details>
			
		<summary>Details</summary>
		  <p>articul: <?php echo $articul; ?></p>
		  <p>charasteristics: <?php echo $charasteristics; ?></p>
		  <p>description: <?php echo $description; ?></p>
		  <p>discount: <?php echo $discount; ?></p>
		  <p>price: <?php echo $price; ?></p>
		</details>
		<?php
		if(is_single()){

			the_content();

		} else {

			if($excerpt_display == 'full-content'){

				the_content( sprintf(
				wp_kses(
					/* translators: %s: Name of current post. Only visible to screen readers */
					$excerpt_text. '<span class="screen-reader-text"> "%s"</span>',
					array(
						'span' => array(
							'class' => array(),
						),
					)
				),
				get_the_title()
			) );
			} else {
				the_excerpt();
			}
		}

		wp_link_pages( array(
			'before' => '<div class="page-links">' . esc_html__( 'Pages:', 'timesnews' ),
			'after'  => '</div>',
		) );

		if (! is_single() || ($disable_featured_image_single ==0 && is_single() ) ):

		timesnews_post_thumbnail();

		endif;

		?>

	</div><!-- .entry-content -->

	<?php if( $disable_comments ==0 || !empty($tags_list) ) { ?>

		<footer class="entry-footer">
			<div class="entry-meta">

				<?php
				if(!empty($tags_list) ) {

					timesnews_tag_lists();

				}
				
				if( $disable_comments ==0 ) {

					timesnews_comment_links();

				}

				?>
			</div><!-- .entry-meta -->
		</footer><!-- .entry-footer -->
			
	<?php } ?>
	<button product-id="<?php the_ID(); ?>" class="add-to-bucket">Add</button>
	<button product-id="<?php the_ID(); ?>" class="remove-from-bucket">Remove</button>
	<p product-id="<?php the_ID(); ?>" class="amount"></p>
	</div><!-- .entry-content-holder -->
</article><!-- #post-<?php the_ID(); ?> -->
